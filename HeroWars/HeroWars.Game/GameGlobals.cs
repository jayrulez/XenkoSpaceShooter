using SiliconStudio.Xenko.Engine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroWars
{
    static class GameGlobals
    {
        public static EventKey PlayerDeathEventKey = new EventKey("Global", "PlayerDeath");
        public static EventKey GameOverEventKey = new EventKey("Global", "GameOver");
    }
}
