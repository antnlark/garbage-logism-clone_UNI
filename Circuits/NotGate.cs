using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    /// <summary>
    /// This subclass implements a NOT gate with a single input and single output,
    ///  drawn as a triangle with a small bubble at the tip showing inversion.
    /// </summary>
    public class NotGate : Gate
    {
        //Gap const to uniformly place pins correctly.
        private const int GAP = 10;

        //Bubble diameter const used to size the inversion bubble.
        private const int BUBBLE_DIAMETER = 10;

        /// <summary>
        /// Creates a new NOT gate with one input pin and one output pin.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public NotGate(int x, int y)
        {
            width = 40;
            height = 40;
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, false, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the gate as a filled triangle plus a small circular
        ///  bubble at the tip, indicating inversion.
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

            //Triangle body
            Point top = new Point(left, this.top);
            Point bottom = new Point(left, this.top + height);
            Point tip = new Point(left + width - BUBBLE_DIAMETER, this.top + height / 2);
            Point[] trianglePoints = { top, bottom, tip };

            paper.FillPolygon(brush, trianglePoints);
            paper.FillEllipse(brush, left + width - BUBBLE_DIAMETER, this.top + height / 2 - BUBBLE_DIAMETER / 2, BUBBLE_DIAMETER, BUBBLE_DIAMETER);
        }

        /// <summary>
        /// Moves the gate and positions its pins on the left and right edges.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            left = x;
            top = y;
            pins[0].X = x - GAP;
            pins[0].Y = y + height / 2;
            pins[1].X = x + width + GAP;
            pins[1].Y = y + height / 2;
        }

        /// <summary>
        /// Returns the logical inverse of the single input pin.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            return !EvaluatePin(pins[0]);
        }

        /// <summary>
        /// Creates and independent copt of this NOT gate.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            return new NotGate(left, top);
        }
    }
}
