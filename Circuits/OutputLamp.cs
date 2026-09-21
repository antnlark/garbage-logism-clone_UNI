using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    /// <summary>
    /// This subclass implements a gate with a single input and no outputs,
    ///  storing a boolean value that reflects the result of its last Evaluate() call.
    /// </summary>
    public class OutputLamp : Gate
    {
        //Gap const to uniformly place pins correctly.
        private const int GAP = 10;

        //Protected bool var for the state of the gate.
        protected bool state = false;
        
        /// <summary>
        /// Creates a new OutputLamp with one input pin.
        ///  Starts dark (state = false) until evaluated.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public OutputLamp(int x, int y)
        {
            width = 40;
            height = 40;
            pins.Add(new Pin(this, true, 20));
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the gate as a circle: red while selected, yellow when true, gray when false.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            Brush brush;
            //If chain to check which brush to use.
            if (selected)
                brush = selectedBrush;
            else if (state)
                brush = Brushes.Yellow;
            else
                brush = normalBrush;

            foreach (Pin p in pins)
                p.Draw(paper);

            paper.FillEllipse(brush, left, top, width, height);
        }

        /// <summary>
        /// Moves the gate and positions its single input pin on the left edge.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            left = x;
            top = y;
            pins[0].X = x - GAP;
            pins[0].Y = y + height / 2;
        }

        /// <summary>
        /// Evaluates the gate feeding this lamp's input,
        ///  stores the result in state so Draw can show it,
        ///   and returns that result.
        /// </summary>
        /// <returns></returns>
        public override bool Evaluate()
        {
            state = EvaluatePin(pins[0]);
            return state;
        }

        /// <summary>
        /// Creates and independent copy of this OutputLamp.
        ///  The clone starts dark (state = false) until evaluated.
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            return new OutputLamp(left, top);
        }
    }
}
