using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circuits
{
    /// <summary>
    /// Abstract superclass for every kind of logic gate in the circuit
    ///  (AndGate, OrGate, NotGate, InputSource, OutputLamp, and Compound).
    ///  Holds the state and behaviour common to all gates - position, pins,
    ///  selection; and declares the operations every gate must provide its
    ///  own implementation of.
    /// </summary>
    public abstract class Gate
    {
        //Left property of the gate.
        protected int left;

        //Top property of the gate.
        protected int top;

        //Width property of the gate.
        protected int width;

        //Height property of the gate.
        protected int height;

        //Red brush for selected components.
        protected Brush selectedBrush = Brushes.Red;

        //Normal brush (light gray) for deselected/normal components.
        protected Brush normalBrush = Brushes.LightGray;

        //Protected list of pins.
        protected List<Pin> pins = new List<Pin>();

        //Protected bool var for selected and not selected components.
        protected bool selected = false;

        /// <summary>
        /// Indicates whether this gate is the currently selected gate.
        ///  Virtual so subclasses can add their own behavior if selection logic changes.
        /// </summary>
        public virtual bool Selected
        {
            get {  return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// The left-hand edge of the gate.
        /// </summary>
        public int Left
        {
            get { return left; }
        }

        /// <summary>
        /// The top edge of the gate.
        /// </summary>
        public int Top
        {
            get { return top; }
        }

        /// <summary>
        /// The list of pins belonging to this gate.
        /// </summary>
        public List<Pin> Pins
        {
            get { return pins; }
        }

        /// <summary>
        /// Evaluates the gate feeding into pin p.
        ///  Treating an unconnected pin as false and warning the user.
        ///   Shared by every gate type's Evaluate() so the same check isn't repeated. 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected bool EvaluatePin(Pin p)
        {
            if (p.InputWire == null)
            {
                MessageBox.Show("Error: an input pin is not connected.");
                return false;
            }
            Gate source = p.InputWire.FromPin.Owner;
            return source.Evaluate();
        }

        /// <summary>
        /// Checks whether 'x' and 'y' lies within this gate's bounding box.
        ///  Virtual so Compound.cs can override it to check its grouped gates instead.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual bool IsMouseOn(int x, int y)
        {
            if (left <= x && x < left + width
                && top <= y && y < top + height)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Sets this gate's visual 'selected' appearance directly, without
        ///  going through the Selected property. Used when a gate is swept
        ///  up as part of a group so it visually highlights without triggering
        ///   any selection side-effects a sublass may define.
        ///   Virtual so Compound.cs can ovveride it to recurse into its members.
        /// </summary>
        /// <param name="on"></param>
        public virtual void Highlight(bool on = true)
        {
            selected = on;
        }

        /// <summary>
        /// Draws the gate. Every subclass must provide its own visual representation.
        /// </summary>
        /// <param name="paper"></param>
        public abstract void Draw(Graphics paper);

        /// <summary>
        /// Moves the gate (and its pins) to the position specified.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public abstract void MoveTo(int x, int y);

        /// <summary>
        /// Computes this gate's output based on its inputs.
        ///  InputSource returns its stored value directly;
        ///   every other gate recurses into whatever feeds its input pins.
        /// </summary>
        /// <returns></returns>
        public abstract bool Evaluate();

        /// <summary>
        /// Creates an independent copy of this gate, includings its own new set of pins.
        /// </summary>
        /// <returns></returns>
        public abstract Gate Clone();
    }
}
