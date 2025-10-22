namespace ItsALittleGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var loop = new GameLoop(12);

            loop.Run(game.Update, game.Render);
        }
    }
}
