using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    /// <summary>
    /// This subclass implements a Gate made of other Gates grouped together,
    ///  so they can be moved, selected, and cloned as a single unit.
    ///   A Compound may intself contain other Compound gates, forming a tree.
    /// </summary>
    public class Compound : Gate
    {
        //Proteced own gates list to handle grouped gates.
        protected List<Gate> gates = new List<Gate>();

        //Proteced internal wires list to handle wires within the compound.
        public List<Wire> InternalWires = new List<Wire>();

        /// <summary>
        /// Gets the protected gates list
        /// </summary>
        public List<Gate> Gates
        {
            get { return gates; }
        }

        /// <summary>
        /// Adds a gate to this compound. The first gate added anchors the
        ///  compounds, own left/top, since Compound has no size of its own.
        ///  Gives MoveTo a real starting position to measure movement from.
        /// </summary>
        /// <param name="g"></param>
        public void AddGate(Gate g)
        {
            if (gates.Count == 0)
            {
                left = g.Left;
                top = g.Top;
            }
            gates.Add(g);
        }

        /// <summary>
        /// Draws every gate in this compound.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            foreach (Gate g in gates)
                g.Draw(paper);
        }

        /// <summary>
        /// Moves the whole compound by shifting every member gate by the same offset,
        ///  preserving their positions relative to each other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            int dx = x - left;
            int dy = y - top;
            left = x;
            top = y;

            foreach (Gate g in gates)
            {
                g.MoveTo(g.Left + dx, g.Top + dy);
            }
        }

        /// <summary>
        /// Returns true if (x, y) is over any gate in this compound,
        ///  since a Compound has no bounding box of its own.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool IsMouseOn(int x, int y)
        {
            foreach (Gate g in gates)
            {
                if (g.IsMouseOn(x, y))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Not supported: a Compound has no single well defined output,
        ///  since it may contain several OutputLamps or none. Evaulation
        ///  is instead performed per lmap via Form1's revursive search.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool Evaluate()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Selecting a compound cascades to every member via Highlight,
        ///  so the whole group visually highlights without triggering
        ///  subclass specific selction side effects.
        /// </summary>
        public override bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                foreach (Gate g in gates)
                {
                    g.Highlight(value);
                }
            }
        }

        /// <summary>
        /// Highlights or un-highlights every gate in this compound,
        ///  including gates inside any nested Compounds.
        /// </summary>
        /// <param name="on"></param>
        public override void Highlight(bool on = true)
        {
            selected = on;
            foreach (Gate g in gates)
            {
                g.Highlight(on);
            }
        }

        /// <summary>
        /// Creates an independent copy of this compound: every member gate
        ///  is cloned (recursing into nested Compounds), then every wire
        ///  that is fully internal to this compound's tree - at any nesting depth on either end -
        ///  is rebuilt between the corresponding clones. A wire with one end outside this compound
        ///  is left connected to the original gate.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            Dictionary<Gate, Gate> originalToClone = new Dictionary<Gate, Gate>();
            Compound clone = CloneStructure(originalToClone);
            RebuildWires(originalToClone, clone);
            return clone;
        }

        /// <summary>
        /// Recursivly clones every gate in this compound, including gates
        ///  inside nested Compounds, without reconnecting any wires yet.
        ///  Every cloned LEAF gate Onot itself a Compound) is recorded in
        ///  orignalToClone so RebuildWires can look it up afterwards.
        /// </summary>
        /// <param name="originalToClone"></param>
        /// <returns></returns>
        private Compound CloneStructure(Dictionary<Gate, Gate> originalToClone)
        {
            Compound clone = new Compound();
            foreach (Gate original in gates)
            {
                Gate copy;
                if (original is Compound subCompound)
                {
                    //Recurse directly rather than calling original.Clone(),
                    // so wiring is only ever rebuilt once, in one final pass,
                    // instead of once per nesting level.
                    copy = subCompound.CloneStructure(originalToClone);
                }
                else
                {
                    copy = original.Clone();
                    originalToClone[original] = copy;
                }
                clone.AddGate(copy);
            }
            return clone;
        }

        /// <summary>
        /// Recursively walks this compound's original gates (including
        ///  nested Compounds) and rebuilds any wire whose source gate is also present
        ///  in originalToClone. i.e. - any wire that is fully internal to the tree
        ///  being cloned, however deeply either end is nested.
        ///  A wire with one end outside this compound is deliberately left connected to the original gate.
        /// </summary>
        /// <param name="originalToClone"></param>
        /// <param name="cloneRoot"></param>
        private void RebuildWires(Dictionary<Gate, Gate> originalToClone, Compound cloneRoot)
        {
            //Walk every gate that was directly added to THIS compound.
            // May themsevles be nested compounds.
            foreach (Gate original in gates)
            {
                if (original is Compound subCompound)
                {
                    // If this member is itself a compound, dont try to read
                    // its pins directly.
                    subCompound.RebuildWires(originalToClone, cloneRoot);
                    continue;
                }

                Gate originalClone = originalToClone[original];

                // Check every pin on the orignal gate.
                for (int i = 0; i < original.Pins.Count; i++)
                {
                    Pin originalPin = original.Pins[i];

                    // Only input pins can have a wire coming INTO them,
                    // and only bother if something is actually connected.
                    if (!originalPin.IsInput || originalPin.InputWire == null)
                        continue;

                    // Find out which pin is feeding this input, on the original circuit.
                    Pin sourcePin = originalPin.InputWire.FromPin;
                    Gate sourceGate = sourcePin.Owner;

                    if (originalToClone.ContainsKey(sourceGate))
                    {
                        Gate clonedSourceGate = originalToClone[sourceGate];
                        int sourcePinIndex = sourceGate.Pins.IndexOf(sourcePin);

                        Pin clonedInputPin = originalClone.Pins[i];
                        Pin clonedOutputPin = clonedSourceGate.Pins[sourcePinIndex];

                        //Build a brand new wire connected the two CLONED pins.
                        Wire newWire = new Wire(clonedOutputPin, clonedInputPin);
                        clonedInputPin.InputWire = newWire;
                        cloneRoot.InternalWires.Add(newWire);
                    }
                }
            }
        }
    }
}
