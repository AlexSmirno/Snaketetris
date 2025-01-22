using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snaketetris.GameLogic
{
    public class Direction
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public void MoveUp()
        {
            X = 0;
            Y = -1;
        }

        public void MoveRight()
        {
            X = 1;
            Y = 0;
        }

        public void MoveDown()
        {
            X = 0;
            Y = 1;
        }

        public void MoveLeft()
        {
            X = -1;
            Y = 0;
        }

        public void Stop()
        {
            X = 0;
            Y = 0;
        }
    }
}
