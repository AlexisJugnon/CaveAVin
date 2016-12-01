using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Position
    {

        private int x;
        private int y;
        private int casier;

        public Position(int x2 = -1,int y2 = -1)
        {
            X = x2;
            Y = y2;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int Casier
        {
            get
            {
                return casier;
            }

            set
            {
                casier = value;
            }
        }
    }
}
