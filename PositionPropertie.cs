using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsALittleGame
{
    public class PositionPropertie
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PositionPropertie(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }
}
