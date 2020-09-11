using System;

namespace project_freyr
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new ProjectFreyr(200);
            game.Run();
        }
    }
}
