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
    /// This subclass implements an OR gate with two inputs and one output,
    ///  drawn as a curved shield shape rather than AndGate's circle and rectangle.
    /// </summary>
    public class OrGate : Gate
    {
        //Gap const to uniformly place pins correctly.
        private const int GAP = 10;

        /// <summary>
        /// Creates a new OR gate with two input pins and one output pin,
        ///  positioned at (x, y). Pin layout matches AndGate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public OrGate(int x, int y)
        {
            width = 40;
            height = 40;
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, false, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the gate's curved outline using Bezier curves,
        ///  filled with the selected or normal brush. The gates design was found on a forum.
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

            //Drawing a much better or gate rather than the primitive shapes.
            using (GraphicsPath path = new GraphicsPath())
            {
                // Points that sketch the OR shape
                Point backTop = new Point(left, top);
                Point backBottom = new Point(left, top + height);
                Point tip = new Point(left + width, top + height / 2);
                Point backCurveIn = new Point(left + width / 6, top + height / 2); // the concave on the left

                // Curve along the top from back-top to the tip
                path.AddBezier(
                    backTop,
                    new Point(left + width / 2, top),
                    new Point(left + width * 3 / 4, top + height / 6),
                    tip);

                // Curve along the bottom from the tip back to back-bottom
                path.AddBezier(
                    tip,
                    new Point(left + width * 3 / 4, top + height * 5 / 6),
                    new Point(left + width / 2, top + height),
                    backBottom);

                // Concave curve closing the shape on the left (input) side
                path.AddBezier(
                    backBottom,
                    backCurveIn,
                    backCurveIn,
                    backTop);

                paper.FillPath(brush, path);
            }
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
        /// Returns true if either input pin evaluates to true.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            return EvaluatePin(pins[0]) || EvaluatePin(pins[1]);
        }

        /// <summary>
        /// Creates an independent copy of this OR gate.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            return new OrGate(left, top);
        }
    }
}
