using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroWars.GameData
{
    public class GameDataStore
    {
        private static GameDataStore _instance;

        public static GameDataStore Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameDataStore();
                }

                return _instance;
            }
        }

        public PlayerProfile GetProfile()
        {
            return new PlayerProfile();
        }
    }
}
