using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ItsALittleGame
{
    internal class Player
    {
        public int X { get; set; } = 10;
        public int Y { get; set; } = 5;

        private int prevY = 10;
        private int prevX = 5;
        public int Health { get; set; }
        int WalkingSpeed { get; set; } = 2;

        char tempSymbol = 'h';

        public Player()
        {

        }

        public void PlacePlayer()
        {
            Console.SetCursorPosition(prevX, prevY);

            //Handle everything
            if (X < Console.WindowWidth && X > 0 && Y < Console.WindowHeight && Y > 0)
            {
                Console.Write(" ");
                Console.SetCursorPosition(X, Y);
                Console.Write(tempSymbol);
            }
            //If something isn't right
            else
            {
                if (X > Console.WindowWidth)
                {
                    X = Console.WindowWidth - 1;
                }
                else if (X < 0)
                {
                    X = 1;
                }

                if (Y > Console.WindowHeight)
                {
                    Y = Console.WindowHeight - 1;
                }
                else if (Y < 0)
                {
                    Y = 1;
                }

                Console.Write(" ");
                Console.SetCursorPosition(X, Y);
                Console.Write(tempSymbol);
            }

            prevY = Y;
            prevX = X;
        }
        public void Move(int dx, int dy)
        {

            int newX = X + dx;
            int newY = Y + dy;

            // kolla gränser
            if (newX >= 0 && newX < Console.WindowWidth &&
                newY >= 0 && newY < Console.WindowHeight)
            {
                X = newX;
                Y = newY;
            }
        }

    }
}
