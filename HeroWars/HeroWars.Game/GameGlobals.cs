using Xenko.Engine.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroWars
{
    static class GameGlobals
    {
        public static EventKey PlayerDeathEventKey = new EventKey("Global", "PlayerDeath");
        public static EventKey PlayerDamageEventKey = new EventKey("Global", "PlayerDamage");
        public static EventKey EnemyDeathEventKey = new EventKey("Global", "EnemyDeath");
        public static EventKey GameOverEventKey = new EventKey("Global", "GameOver");
        
        public static int GetHighScore()
        {
            try
            {
                int score = 0;

                var scoreText = File.ReadAllText("score.txt");

                if (!string.IsNullOrEmpty(scoreText))
                {
                    if (int.TryParse(scoreText, out score))
                    {
                        return score;
                    }
                    else
                    {
                        score = 0;
                    }
                }

                return score;
            }catch(Exception)
            {
                return 0;
            }
        }
        
        public static void SetHighScore(int score)
        {
            File.WriteAllText("score.txt", score.ToString());
        }
    }
}
