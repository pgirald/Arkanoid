using System;

namespace Arkanoid
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ArkanoidGame())
                game.Run();
        }
    }
}
