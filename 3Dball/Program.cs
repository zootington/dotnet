using OpenTK;

namespace DimBall
{
    public static class Program
    {
        [STAThread] // This attribute ensures the application runs in single-threaded apartment mode, which can be important for some functions like drag-and-drop.
        public static void Main()
        {
            using (MyGameWindow game = new MyGameWindow()) 
            {
                game.Run(60.0); // This will run the game at 60 updates per second.
            }
        }
    }
}
