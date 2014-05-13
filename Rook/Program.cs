using System;

namespace Rook
{
#if WINDOWS || XBOX
    static class Program
    {
        // The main entry point for the application.
        static void Main(string[] args)
        {
            using (Main game = new Main())
            {
                game.Run();
            }
        }
    }
#endif
}

