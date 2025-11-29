using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ItsALittleGame
{
    internal class Player
    {

        public Bounds.Coordinate PlayerPosition { get; set; }
        public Bounds.Coordinate PlayerSize { get; private set; }
        List<List<Bounds.Coordinate>> playerBouningBox = new List<List<Bounds.Coordinate>>();
        private Bounds.Coordinate _playerPrevPosition;

        private int screenTop;
        private int screenRight;
        private int screenLeft;
        private int screenBottom;

        public int Health { get; set; }
        int WalkingSpeed { get; set; } = 1;

        //Animation frames
        int runFrames = 6;
        int currentRunFrame;
        int climbFrames = 2;
        int currentClimbFrame;
        int jumpFrames = 3;
        int currentJumpFrame;

        int prevSprite;


        public Player()
        {
            PlayerSize = new Bounds.Coordinate(3,6);
            PlayerPosition = new Bounds.Coordinate(10, 50);
            _playerPrevPosition = new Bounds.Coordinate(10,50);

            for (int i = 0; i < PlayerSize.Y; i++)
            {
                playerBouningBox.Add(new List<Bounds.Coordinate>());

                for (int j = 0; j < PlayerSize.X; j++)
                {
                    playerBouningBox.Last().Add(new Bounds.Coordinate(PlayerPosition.X + j, PlayerPosition.Y + j));                 
                }
            }
        }

        public void SetPlayerBoundingBox()
        {
        }

        public void SetPlayerScreenBounds(int top, int right, int left, int bottom) //This is the bounds of the borders, not the player.
        {
            screenBottom = bottom - 1; //Added some cheeky adjustments here to take player "fatness" into account
            screenRight = right - 3;
            screenLeft = left;
            screenTop = top;
        }

        void WriteColor(string text, ConsoleColor color) //Just makes it easier for me to make the leggies on the player to switch colors
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void AnimatePlayer(int dirX, int dirY)
        {
            //Erases old sprite
            Console.SetCursorPosition(_playerPrevPosition.X, _playerPrevPosition.Y);      // Upper left
            Console.Write("       ");
            Console.SetCursorPosition(_playerPrevPosition.X, _playerPrevPosition.Y + 1);  // Middle left
            Console.Write("       ");
            Console.SetCursorPosition(_playerPrevPosition.X, _playerPrevPosition.Y + 2);  // Lower left
            Console.Write("       ");
            _playerPrevPosition.Y = PlayerPosition.Y;
            _playerPrevPosition.X = PlayerPosition.Y;

            // 0 walk right
            // 1 walk left
            // 2 climb
            // 3 jump

            //Check 

            if (dirX == 1) //If player is moving right
            {
                if (prevSprite == 0)
                {
                    currentRunFrame = (currentRunFrame + 1 > runFrames) ? 1 : currentRunFrame + 1;                    
                }
                PlayerFrames(0, currentRunFrame);
                prevSprite = 0;
            }
            else if (dirX == -1) //If the player is moving left
            {
                if (prevSprite == 1)
                {
                    currentRunFrame = (currentRunFrame + 1 > runFrames) ? 1 : currentRunFrame + 1;
                }
                PlayerFrames(1, currentRunFrame);
                prevSprite = 1;
            }
            else if (dirY == -1)
            {
                if (prevSprite == 2)
                {
                    currentClimbFrame = (currentClimbFrame + 1 > climbFrames) ? 1 : currentClimbFrame + 1;
                }
                PlayerFrames(2, currentClimbFrame);
                prevSprite = 2;
            }
            else if (dirY == 1)
            {
                if (prevSprite == 3)
                {
                    currentJumpFrame = (currentJumpFrame + 1 > jumpFrames) ? 1 : currentJumpFrame + 1;
                }
                PlayerFrames(3, currentJumpFrame);
                prevSprite = 3;
            }
            else //Idle
            {
                PlayerFrames(5, 0);
            }
        }
        public void PlayerFrames(int state, int frame)
        {
            switch (state)
            {
                //Walking right ---->
                case 0:
                    switch (frame)
                    {
                        case 1:
                            // ( ._.)
                            //  /|\
                            //   |> 
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ( ._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  /|", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("   |", ConsoleColor.DarkGray);
                            WriteColor(">", ConsoleColor.White);
                                                        
                            break;
                        case 2:
                            // ( ._.)
                            //  /|\
                            //  />
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ( ._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  /|", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  /", ConsoleColor.DarkGray);
                            WriteColor(">", ConsoleColor.White);
                            break;
                        case 3:
                            // ( ._.)
                            //  /|\
                            //  /\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ( ._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  /|", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  /", ConsoleColor.DarkGray);
                            WriteColor("\\", ConsoleColor.White);
                            break;
                        case 4:
                            // ( ._.)
                            //  /|\
                            //   |>
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ( ._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  /", ConsoleColor.DarkGray);
                            WriteColor("|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("   |", ConsoleColor.White);
                            WriteColor(">", ConsoleColor.DarkGray);
                            break;
                        case 5:
                            // ( ._.)
                            //  /|\
                            //  />
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ( ._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor(" /", ConsoleColor.DarkGray);
                            WriteColor("|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  /", ConsoleColor.White);
                            WriteColor(">", ConsoleColor.DarkGray);
                            break;
                        case 6:
                            // ( ._.)
                            //  /|\
                            //  /\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ( ._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  /", ConsoleColor.DarkGray);
                            WriteColor("|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  /", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            break;
                        
                    }
                    break;
                //Walking left <-------
                case 1:
                    switch (frame)
                    {
                        case 1:
                            // (._. )
                            //   /|\
                            //   <| 
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" (._. )", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   /|", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("   <", ConsoleColor.DarkGray);
                            WriteColor("|", ConsoleColor.White);
                            break;
                        case 2:
                            // (._. )
                            //   /|\
                            //    <\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" (._. )", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   /|", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("    <", ConsoleColor.DarkGray);
                            WriteColor("\\", ConsoleColor.White);
                            break;
                        case 3:
                            // (._. )
                            //   /|\
                            //    /\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" (._. )", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   /|", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("    /", ConsoleColor.DarkGray);
                            WriteColor("\\", ConsoleColor.White);
                            break;
                        case 4:
                            // (._. )
                            //   /|\
                            //   <|
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" (._. )", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   /", ConsoleColor.DarkGray);
                            WriteColor("|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("   <", ConsoleColor.White);
                            WriteColor("|", ConsoleColor.DarkGray);
                            break;
                        case 5:
                            // (._. )
                            //   /|\
                            //    <\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" (._. )", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   /", ConsoleColor.DarkGray);
                            WriteColor("|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("    <", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            break;
                        case 6:
                            // (._. )
                            //   /|\
                            //    /\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" (._. )", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   /", ConsoleColor.DarkGray);
                            WriteColor("|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("    /", ConsoleColor.White);
                            WriteColor("\\", ConsoleColor.DarkGray);
                            break;
                    }
                    break;

                //Climbing
                case 2:

                    switch(frame)
                    {
                        case 1:
                            // ('-')
                            //  /|^
                            //  ^ \
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ('-')", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  /|^", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  ^ \\", ConsoleColor.White);
                            break;

                        case 2:
                            // ('-')
                            //  ^|\
                            //  / ^
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ('-')", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("  ^|\\", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  / ^", ConsoleColor.White);
                            break;
                    }
                    break;

                //Jumping
                case 3:
                    switch (frame)
                    {
                        case 1:
                            // ('-')
                            //  /|\
                            //  /\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor(" ('-')", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X,PlayerPosition.Y + 1);
                            WriteColor("  /|\\^", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  /\\", ConsoleColor.White);
                            break;

                        case 2:
                            // \(._.)/
                            //    |
                            //   ^ ^
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor("\\(._.)/", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor("   |", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor("  ^ ^", ConsoleColor.White);
                            break;

                        case 3:
                            // 
                            // (._.)
                            // /< >\
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                            WriteColor("", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                            WriteColor(" (._.)", ConsoleColor.White);
                            Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                            WriteColor(" /< >\\", ConsoleColor.White);
                            break;
                    }
                    break;
                default:
                    // (._.)
                    //  /|\
                    //  /\
                    Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y);
                    WriteColor(" (._.)", ConsoleColor.White);
                    Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 1);
                    WriteColor("  /|\\", ConsoleColor.White);
                    Console.SetCursorPosition(PlayerPosition.X, PlayerPosition.Y + 2);
                    WriteColor("  /\\", ConsoleColor.White);
                    break;

            }
        }

        public void Move(int dx, int dy)
        {
            int newX = PlayerPosition.X + (dx * WalkingSpeed) * 2; //Multeplies the direction with the walk speed (-1 * speed) (1 * speed). Adds * 2 cuz a symbol is about twice as tall as it is wide. 
            int newY = PlayerPosition.Y + (dy * WalkingSpeed);

            // Check bounds
            if (newX >= screenLeft && newX < screenRight - 4 &&
                newY >= screenTop && newY < screenBottom - 1)
            {
                PlayerPosition = new Bounds.Coordinate(newX, newY);
            }
        }

    }
}
