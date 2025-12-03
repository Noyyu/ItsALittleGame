using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ItsALittleGame
{
    internal class PickUp
    {
        private string PickUpType { get; set; }
        public Coordinate PickUpPosition { get; private set; }
        public List<List<Coordinate>> BoundingBox = new List<List<Coordinate>>();

        public PickUp(string pickUpType)
        {
            switch (pickUpType)
            {
                case "Bunny":
                    BoundingBox = Bounds.CreateBoundingBox(5, 3);
                    PickUpType = "Bunny";
                    break;
                case "Mouse":
                    BoundingBox = Bounds.CreateBoundingBox(3, 1);
                    PickUpType = "Mouse";
                    break;
                default:
                    break;
            }
        }

        public void SetPosition(Coordinate position)
        {
            PickUpPosition = position;

            PickUpPosition = new Coordinate()
            {
                X = Math.Clamp(position.X, 1, 106),
                Y = Math.Clamp(position.Y, 1, 18)
            };
        }
        public void SetPickUpType(string type)
        {
            PickUpType = type;
        }
        public void PrintPickup()
        {
            switch (PickUpType)
            {
                case "Mouse":
                    Console.SetCursorPosition(PickUpPosition.X, PickUpPosition.Y);
                    Console.WriteLine("ᘛ⁐̤ᕐᐷ");
                    break;

                case "Bunny":
                    Console.SetCursorPosition(PickUpPosition.X, PickUpPosition.Y);
                    Console.WriteLine("(\\/)");
                    Console.SetCursorPosition(PickUpPosition.X, PickUpPosition.Y + 1);
                    Console.WriteLine("(._.)");
                    Console.SetCursorPosition(PickUpPosition.X, PickUpPosition.Y + 2);
                    Console.WriteLine("/>❤️");
                    break;
                default:
                    //nothing
                    break;
            }
        }
    }
}
