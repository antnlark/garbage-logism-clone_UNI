using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    /// <summary>
    /// This class implements an AND gate with two inputs
    /// and one output.
    /// </summary>
    public class AndGate : Gate
    {
        // Completely rewritten ( old code saved on doc for reference)
        // Draw and MoveTo method kept the same.

        //Gap const to uniformly place pins correctly.
        private const int GAP = 10;

        /// <summary>
        /// Creates a new AND gate with two input pins and one output pin, positioned at (x, y).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public AndGate(int x, int y)
        {
            width = 40;
            height = 40;
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, false, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the gate in its respective colour.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            Brush brush;
            
            //Check if the gate has been selected
            if (selected)
            {
                brush = selectedBrush;
            }
            else
            {
                brush = normalBrush;
            }

            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            // AND is simple, so we can use a circle plus a rectangle
            // An alternative would be to use a bitmap.
            paper.FillEllipse(brush, left, top, width, height);
            paper.FillRectangle(brush, left, top, width / 2, height);
        }

        /// <summary>
        /// Moves the gate to the position specified.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            //Debugging message
            Console.WriteLine("pins = " + pins.Count);
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
            //Must move the pins too
            pins[0].X = x - GAP;
            pins[0].Y = y + GAP;
            pins[1].X = x - GAP;
            pins[1].Y = y + height - GAP;
            pins[2].X = x + width + GAP;
            pins[2].Y = y + height / 2;
        }

        /// <summary>
        /// Returns true only if both input pins evaluate to true.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            return EvaluatePin(pins[0]) && EvaluatePin(pins[1]);
        }

        /// <summary>
        /// Creates an independent copy of this AND gate.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            return new AndGate(left, top);
        }
    }
}
