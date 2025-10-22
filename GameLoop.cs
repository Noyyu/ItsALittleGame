using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsALittleGame
{
    internal class GameLoop
    {
        private readonly int targetFPS;
        private readonly double frameTimeMs;
        public bool EndGame { get; set; } = false;


        public GameLoop(int tagetFPS = 12)
        {
            this.targetFPS = tagetFPS; //Makes the game run at 12~ frames per second
            frameTimeMs = 1000.0 / targetFPS;
        }

        public void Run(Action update, Action render) //Gives this method acces to other methods (update and render) that isn't returning anything (void).
        {
            while (!EndGame)
            {
                var stopWatch = System.Diagnostics.Stopwatch.StartNew(); //Start timer

                // Run game

                update();
                render();

                // 

                stopWatch.Stop(); //Stop timer, will measure the amount of time the loop took

                double elapsed = stopWatch.Elapsed.TotalMilliseconds; //Gets the time in milliseconds
                int sleep = (int)(frameTimeMs - elapsed); //Calculates for how long the loop should sleep

                if (sleep > 0) //Sleeps if needed
                {
                    Thread.Sleep(sleep);
                }
            }
        }
    }
}
