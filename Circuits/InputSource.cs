using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    /// <summary>
    /// This sublass implements a gate with no inputs and a single output pin,
    ///  storing a boolean value the user can toggle by selecting the gate.
    /// </summary>
    public class InputSource : Gate
    {
        //Gap const to uniformly place pins correctly.
        private const int GAP = 10;

        //Protected bool var for the state of the gate.
        protected bool state = false;

        /// <summary>
        /// Creates a new InputSource with one output pin.
        ///  Starts off (state = false)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public InputSource(int x, int y)
        {
            width = 40;
            height = 40;
            pins.Add(new Pin(this, false, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Overrides the base selection behaviour so that becoming selected
        ///  also toggles this gate's on/off state. Only toggles when selected
        ///  (value == true), not when deselected.
        /// </summary>
        public override bool Selected
        {
            get { return selected; }
            set
            {
                if (value)
                {
                    state = !state;
                }
                base.Selected = value;
            }
        }

        /// <summary>
        /// Draws the gate as a simple square: red while selected, yellow when on, gray when off.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            Brush brush;
            if (selected)
                brush = selectedBrush;
            else if (state)
                brush = Brushes.Yellow;
            else
                brush = normalBrush;

            foreach (Pin p in pins)
                p.Draw(paper);

            paper.FillRectangle(brush, left, top, width, height);
        }

        /// <summary>
        /// Moves the gate and positions its single output pin on the right edge.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            left = x;
            top = y;
            pins[0].X = x + width + GAP;
            pins[0].Y = y + height / 2;
        }

        /// <summary>
        /// Returns the gate's stored on/off value (state) directly
        ///  - the base case of the Evaluate() recursion; has no inputs to evaluate.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            return state;
        }

        /// <summary>
        /// Creates an independent copy of this InputSource at its current position.
        ///  Note the clone always starts off.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            return new InputSource(left, top); //new input defaults to off
        }
    }
}
