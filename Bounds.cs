using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ItsALittleGame
{
    static public class Bounds
    {
        public static bool CheckIntersection(List<List<Vector2>> boundingboxOne, List<List<Vector2>> boundingboxTwo)
        {
            foreach (var coordinate in boundingboxOne)
            {
                if (boundingboxTwo.Contains(coordinate))
                {
                    return true; 
                }
            }

            //bool hasMatch = boundingboxOne.Any(v => boundingboxTwo.Contains(v)); //Apparently the same thing but better :(

            return false;
        }

        static public List<List<PositionPropertie>> CreateBoundingBox(int X, int Y)
        {
            List<List<PositionPropertie>> boundingBox = new List<List<PositionPropertie>>();

            for (int i = 0; i < Y; i++)
            {
                boundingBox.Add(new List<PositionPropertie>());

                for (int j = 0; j < X; j++)
                {
                    boundingBox.Last().Add(new PositionPropertie(X + j, Y + j));
                }
            }
            return boundingBox;
        }


        public struct ScreenBounds
        {
            public int Top { get; private set; }
            public int Right { get; private set; }
            public int Left { get; private set; }
            public int Bottom { get; private set; }

            public ScreenBounds(int right = 0, int buttom = 0)
            {
                Top = 1;
                Left = 1;
                Right = right;
                Bottom = buttom;
            }
        }
    }
}
