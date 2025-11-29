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
        public Bounds.Coordinate PickUpPosition { get; private set; }
        List<List<Bounds.Coordinate>> _pickUpBoundingBox = new List<List<Bounds.Coordinate>>();

        public PickUp(string pickUpType)
        {
            switch (pickUpType)
            {
                case "Bunny":
                    _pickUpBoundingBox = Bounds.CreateBoundingBox(5, 3);
                    break;
                case "Mouse":
                    _pickUpBoundingBox = Bounds.CreateBoundingBox(3, 1);
                    break;
                default:
                    break;
            }
        }

        public void SetPosition(Bounds.Coordinate position)
        {
            PickUpPosition = position;
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
                    Console.SetCursorPosition((int)PickUpPosition.X, (int)PickUpPosition.Y);
                    Console.WriteLine("ᘛ⁐̤ᕐᐷ");
                    break;
                case "Bunny":

                    Console.SetCursorPosition((int)PickUpPosition.X, (int)PickUpPosition.Y);
                    Console.WriteLine("(\\/)");
                    Console.SetCursorPosition((int)PickUpPosition.X, (int)PickUpPosition.Y + 1);
                    Console.WriteLine("(._.)");
                    Console.SetCursorPosition((int)PickUpPosition.X, (int)PickUpPosition.Y + 2);
                    Console.WriteLine("/>❤️");
                    break;
                default:
                    //nothing
                    break;
            }
        }
    }
}
