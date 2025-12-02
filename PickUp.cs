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
        public PositionPropertie PickUpPosition { get; private set; }
        public List<List<PositionPropertie>> _pickUpBoundingBox = new List<List<PositionPropertie>>();

        public PickUp(string pickUpType)
        {
            switch (pickUpType)
            {
                case "Bunny":
                    _pickUpBoundingBox = Bounds.CreateBoundingBox(5, 3);
                    PickUpType = "Bunny";
                    break;
                case "Mouse":
                    _pickUpBoundingBox = Bounds.CreateBoundingBox(3, 1);
                    PickUpType = "Mouse";
                    break;
                default:
                    break;
            }
        }

        public void SetPosition(PositionPropertie position)
        {
            PickUpPosition = position;

            PickUpPosition = new PositionPropertie()
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
