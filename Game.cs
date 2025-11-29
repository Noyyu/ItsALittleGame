using ItsALittleGame;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

internal class Game
{



    // Win32 key polling
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey); //A function that gets raw key data. 

    // Makes some keys
    private const int left = 0x25, up = 0x26, right = 0x27, down = 0x28;
    private const int a = 0x41, w = 0x57, d = 0x44, s = 0x53;


    //Timing management
    private int timeSinceLastUpdateMs = 0;                   // Actual time since last update
    private const int preferedTimeSinceLastUpdateMs = 60;    // Preffered time we want in between updates
    private long loopRunTimeMs = 0;                          // Used to calculate time in between updates. 

    private readonly Stopwatch clock = Stopwatch.StartNew();
    
    private Player player = new Player();
    private PickUp bunny = new PickUp("Bunny");
    private int dirX = 0, dirY = 0;

    Bounds.ScreenBounds screen;
    //Vector2 pickupPos = new Vector2(60, 60);
    Bounds.Coordinate pickupPos = new Bounds.Coordinate(60, 60);

    public Game()
    {
        //Screen
        screen = new Bounds.ScreenBounds(Console.WindowWidth - 10 , Console.WindowHeight - 10);
        DrawBorder();
        Console.SetWindowSize(120, 40);
        Console.CursorVisible = false;
        Console.SetBufferSize(120, 40);

        //Player
        player.SetPlayerScreenBounds(screen.Top, screen.Right, screen.Left, screen.Bottom);


        //pickup
        bunny.SetPosition(new Bounds.Coordinate(50, 20));
        
    }

    public void Update()
    {

        GetPlayerDirection();
        CalculateTimeSinceLastUpdate();
        UpdatePlayerPosition();

    }

    public void Render()
    {
        
        bunny.PrintPickup();
        player.AnimatePlayer(dirX, dirY);

    }

    void DrawBorder()
    {
        // Edges
        Console.SetCursorPosition(screen.Left - 1, screen.Top - 1);     Console.Write('╔'); // Upper Left
        Console.SetCursorPosition(screen.Right + 1, screen.Top - 1);    Console.Write('╗'); // Upper Right
        Console.SetCursorPosition(screen.Left -1, screen.Bottom + 1);   Console.Write('╚'); // Bottom Left
        Console.SetCursorPosition(screen.Right + 1, screen.Bottom +1);  Console.Write('╝'); // Bottom Right

        // top & bottom
        for (int x = screen.Left; x <= screen.Right; x++)
        {
            Console.SetCursorPosition(x, screen.Top - 1);    Console.Write('═');
            Console.SetCursorPosition(x, screen.Bottom + 1); Console.Write('═');
        }
        // sides
        for (int y = screen.Top; y <= screen.Bottom; y++)
        {
            Console.SetCursorPosition(screen.Left -1, y);   Console.Write('║');
            Console.SetCursorPosition(screen.Right + 1, y); Console.Write('║');
        }
    }

    private static bool IsDown(int vKey)
    {
        short keyState = GetAsyncKeyState(vKey);  // Ask windows for the key status
        bool isPressed = (keyState & 0x8000) != 0; // check if its pressed
        return isPressed;                          
    }

    private void UpdatePlayerPosition()
    {
        if ((dirX != 0 || dirY != 0) && timeSinceLastUpdateMs >= preferedTimeSinceLastUpdateMs) //If we have registered a direction AND if its been at least a prefered amount of time since the last update. 
        {
            player.Move(dirX, dirY);
            timeSinceLastUpdateMs = 0; //Resets the timer
        }
    }
    private void CalculateTimeSinceLastUpdate()
    {
        //Clock! It puts the runtime of the loop in "timeAccumulatorMs".
        var now = clock.ElapsedMilliseconds; // Gets current time since game started
        timeSinceLastUpdateMs += (int)(now - loopRunTimeMs); //Calculates how much time its been since the last time this was updated. 
        loopRunTimeMs = now; //Stores "time since game started" to calculate the next loop time. 
    }
    private void GetPlayerDirection()
    {
        dirX = IsDown(left)  || IsDown(a) ? -1
             : IsDown(right) || IsDown(d) ? 1
             : 0;

        dirY = IsDown(up)   || IsDown(w) ? -1
             : IsDown(down) || IsDown(s) ? 1
             : 0;
    }
}
