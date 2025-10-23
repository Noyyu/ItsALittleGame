using ItsALittleGame;
using System;
using System.Diagnostics;
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
    private int dirX = 0, dirY = 0;

    int screenLeft = 1, screenTop = 1;
    int screenRight, screenBottom;

    public Game()
    {
        screenRight = Console.WindowWidth - 2;
        screenBottom = Console.WindowHeight - 2;
        player.SetPlayerScreenBounds(screenTop, screenRight, screenLeft, screenBottom);

        Console.CursorVisible = false;
        // Anpassa gärna men håll det rimligt
        Console.SetWindowSize(120, 40);
        Console.SetBufferSize(120, 40);
    }

    public void Update()
    {

        GetPlayerDirection();
        CalculateTimeSinceLastUpdate();
        UpdatePlayerPosition();

    }

    public void Render()
    {
        DrawBorder();
        player.AnimatePlayer(dirX, dirY);
        //player.PlacePlayer();
    }

    void DrawBorder()
    {
        // Edges
        Console.SetCursorPosition(screenLeft - 1, screenTop - 1);     Console.Write('╔'); // Upper Left
        Console.SetCursorPosition(screenRight + 1, screenTop - 1);    Console.Write('╗'); // Upper Right
        Console.SetCursorPosition(screenLeft -1, screenBottom + 1);   Console.Write('╚'); // Bottom Left
        Console.SetCursorPosition(screenRight + 1, screenBottom +1);  Console.Write('╝'); // Bottom Right

        // top & bottom
        for (int x = screenLeft; x <= screenRight; x++)
        {
            Console.SetCursorPosition(x, screenTop - 1);    Console.Write('═');
            Console.SetCursorPosition(x, screenBottom + 1); Console.Write('═');
        }
        // sides
        for (int y = screenTop; y <= screenBottom; y++)
        {
            Console.SetCursorPosition(screenLeft -1, y);   Console.Write('║');
            Console.SetCursorPosition(screenRight + 1, y); Console.Write('║');
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
