using SiliconStudio.Xenko.Engine;

namespace HeroWars
{
    class HeroWarsApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
