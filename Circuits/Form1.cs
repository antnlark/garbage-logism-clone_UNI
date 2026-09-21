using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



// QUESTIONS

//1. It's better to fully document 'Gate.cs', since thats where the shared contract and design
// decisions live.

//2. An abstract method corces every subclass to provide its own implementation.
// The disadvantage of abstract is it;s less flexible: you cant give subclasses a sensible default behaviour,
// which is why I still used virtual for selected/highlight/ismouseon.

//3. Yes, if a class contains any abstract method, the class itself must be declared abstract too.
// An abstract method has no body, so if the class weren't abstract, you could call 'new Gate()'
// and then call a method that doesn't even exist yet.

//4. Since compound is itself a subclass of 'Gate', adding a compound into another compound works
// automatically for Draw, MoveTo, and selected/highlight, because those all just loop through gates calling
// each members own overridden method. The one place it wasn't automatically robust was 'Clone()', since a nested Compound
// has no pins of its own.



namespace Circuits
{
    /// <summary>
    /// The main GUI for the COMPX102 digital circuits editor.
    /// This has a toolbar, containing buttons called buttonAnd, buttonOr, etc.
    /// The contents of the circuit are drawn directly onto the form.
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The (x,y) mouse position of the last MouseDown event.
        /// </summary>
        protected int startX, startY;

        /// <summary>
        /// If this is non-null, we are inserting a wire by
        /// dragging the mouse from startPin to some output Pin.
        /// </summary>
        protected Pin startPin = null;

        /// <summary>
        /// The (x,y) position of the current gate, just before we started dragging it.
        /// </summary>
        protected int currentX, currentY;

        /// <summary>
        /// The set of gates in the circuit
        /// </summary>
        protected List<Gate> gatesList = new List<Gate>();

        /// <summary>
        /// The set of connector wires in the circuit
        /// </summary>
        protected List<Wire> wiresList = new List<Wire>();

        /// <summary>
        /// The currently selected gate, or null if no gate is selected.
        /// </summary>
        protected Gate current = null;

        /// <summary>
        /// The new gate that is about to be inserted into the circuit.
        /// </summary>
        protected Gate newGate = null;

        /// <summary>
        /// The new compound/group that is about to be inserted into the circuit.
        /// </summary>
        protected Compound newCompound = null;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// Finds the pin that is close to (x,y),
        ///  searching recursively into any grouped Compounds,
        ///  or returns null if there are no pins close to the position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The pin that has been selected</returns>
        public Pin findPin(int x, int y)
        {
            return FindPinIn(gatesList, x, y);
        }

        /// <summary>
        /// Searches a list of gates for a pin near (x, y), recursing into
        ///  any Compound gates so pins nested inside groups can be wired
        ///  directly, at any depth.
        /// </summary>
        /// <param name="gates"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Pin FindPinIn(List<Gate> gates, int x, int y)
        {
            foreach (Gate g in gates)
            {
                foreach (Pin p in g.Pins)
                {
                    if (p.isMouseOn(x, y))
                        return p;
                }
                if (g is Compound c)
                {
                    Pin found = FindPinIn(c.Gates, x, y);
                    if (found != null)
                        return found;
                }
            }
            return null;
        }

        /// <summary>
        /// Handles all events when the mouse is moving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPin != null)
            {
                Console.WriteLine("wire from " + startPin + " to " + e.X + "," + e.Y);
                currentX = e.X;
                currentY = e.Y;
                this.Invalidate();  // this will draw the line
            }
            else if (startX >= 0 && startY >= 0 && current != null)
            {
                Console.WriteLine("mouse move to " + e.X + "," + e.Y);
                current.MoveTo(currentX + (e.X - startX), currentY + (e.Y - startY));
                this.Invalidate();
            }
            else if (newGate != null)
            {
                currentX = e.X;
                currentY = e.Y;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Handles all events when the mouse button is released.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (startPin != null)
            {
                // see if we can insert a wire
                Pin endPin = findPin(e.X, e.Y);
                if (endPin != null)
                {
                    Console.WriteLine("Trying to connect " + startPin + " to " + endPin);
                    Pin input, output;
                    if (startPin.IsOutput)
                    {
                        input = endPin;
                        output = startPin;
                    }
                    else
                    {
                        input = startPin;
                        output = endPin;
                    }
                    if (input.IsInput && output.IsOutput)
                    {
                        if (input.InputWire == null)
                        {
                            Wire newWire = new Wire(output, input);
                            input.InputWire = newWire;
                            wiresList.Add(newWire);
                        }
                        else
                        {
                            MessageBox.Show("That input is already used.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: you must connect an output pin to an input pin.");
                    }
                }
                startPin = null;
                this.Invalidate();
            }
            // We have finished moving/dragging
            startX = -1;
            startY = -1;
            currentX = 0;
            currentY = 0;
        }

        /// <summary>
        /// This will create a new And gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAnd_Click(object sender, EventArgs e)
        {
            newGate = new AndGate(0, 0);
        }

        /// <summary>
        /// Creates a new OR gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOr_Click(object sender, EventArgs e)
        {
            newGate = new OrGate(0, 0);
        }

        /// <summary>
        /// Creates a new NOT gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonNot_Click(object sender, EventArgs e)
        {
            newGate = new NotGate(0, 0);
        }

        /// <summary>
        /// Creates a nnew InputSource gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonInputSource_Click(object sender, EventArgs e)
        {
            newGate = new InputSource(0, 0);
        }

        /// <summary>
        /// Creates a new InputSource gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOutputLamp_Click(object sender, EventArgs e)
        {
            newGate = new OutputLamp(0, 0);
        }

        /// <summary>
        /// Clones the currently selected gate/compund
        ///  and prepares it for placement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (current != null)
            {
                newGate = current.Clone();
            }
        }

        /// <summary>
        /// Evaluates every OutputLamp in the circuit, including lamps
        ///  nested inside any Compound, and redraws to show the results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEvaluate_Click(object sender, EventArgs e)
        {
            EvaluateLamps(gatesList);
            this.Invalidate(); // redraw so lamps show their new state
        }

        /// <summary>
        /// Begins building a new Compound gate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonStartGroup_Click(object sender, EventArgs e)
        {
            newCompound = new Compound();
        }

        /// <summary>
        /// Finishes building the current Compound, clears the temporary
        ///  highlight applied to its members while grouping, and prepares
        ///  it for placement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEndGroup_Click(object sender, EventArgs e)
        {
            if (newCompound != null)
            {
                foreach (Gate g in newCompound.Gates)
                {
                    g.Highlight(false);
                }
            }
            newGate = newCompound;
            newCompound = null;
        }

        /// <summary>
        /// Recursively searches a list of gates - including any nested
        ///  inside Compound gates - and evaluates every OutputLamp found
        ///  at any depth.
        /// </summary>
        /// <param name="gates"></param>
        private void EvaluateLamps(List<Gate> gates)
        {
            foreach (Gate g in gates)
            {
                if (g is OutputLamp)
                {
                    g.Evaluate();
                }
                else if (g is Compound c)
                {
                    EvaluateLamps(c.Gates);
                }
            }
        }

        /// <summary>
        /// Redraws all the graphics for the current circuit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Draw all of the gates
            foreach (Gate g in gatesList)
            {
                g.Draw(e.Graphics);
            }
            //Draw all of the wires
            foreach (Wire w in wiresList)
            {
                w.Draw(e.Graphics);
            }

            if (startPin != null)
            {
                e.Graphics.DrawLine(Pens.White,
                    startPin.X, startPin.Y,
                    currentX, currentY);
            }
            if (newGate != null)
            {
                // show the gate that we are dragging into the circuit
                newGate.MoveTo(currentX, currentY);
                newGate.Draw(e.Graphics);
            }
            if (newCompound != null)
            {
                newCompound.Draw(e.Graphics);
            }
        }

        /// <summary>
        /// Handles events while the mouse button is pressed down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (current == null)
            {
                // try to start adding a wire
                startPin = findPin(e.X, e.Y);
            }
            else if (current.IsMouseOn(e.X, e.Y))
            {
                // start dragging the current object around
                startX = e.X;
                startY = e.Y;
                currentX = current.Left;
                currentY = current.Top;
            }
        }

        /// <summary>
        /// Handles all events when a mouse is clicked in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Check if a gate is currently selected
            if (current != null)
            {
                //Unselect the selected gate
                current.Selected = false;
                current = null;
                this.Invalidate();
            }
            // See if we are inserting a new gate
            if (newGate != null)
            {
                newGate.MoveTo(e.X, e.Y);
                gatesList.Add(newGate);
                if (newGate is Compound compoundGate)
                {
                    wiresList.AddRange(compoundGate.InternalWires);
                }
                newGate = null;
                this.Invalidate();
            }
            else if (newCompound != null)
            {
                foreach (Gate g in gatesList)
                {
                    if (g.IsMouseOn(e.X, e.Y))
                    {
                        //g.Selected = true;
                        g.Highlight();
                        newCompound.AddGate(g);
                        gatesList.Remove(g);
                        this.Invalidate();
                        break;
                    }
                }
            }
            else
            {
                // search for the first gate under the mouse position
                foreach (Gate g in gatesList)
                {
                    if (g.IsMouseOn(e.X, e.Y))
                    {
                        g.Selected = true;
                        current = g;
                        this.Invalidate();
                        break;
                    }
                }
            }
        }
    }
}
