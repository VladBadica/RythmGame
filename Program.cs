using System;

namespace RythmGame
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using var game = new Main();
            game.Run();
        }
    }
}

