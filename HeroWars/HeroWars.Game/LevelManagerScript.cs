// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Input;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Physics;

namespace HeroWars
{
    public class LevelManagerScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        
        public float SpawnInterval {get; set; }
        public float SpawnCountdown { get; set; }

        private Prefab EnemyShipPrefab;

        public override void Start()
        {
            // Initialization of the script.

            EnemyShipPrefab = Content.Load<Prefab>("EnemyShip");

            SpawnInterval = 1f;

            SpawnCountdown = 0;
        }

        public override void Update()
        {
            // Do stuff every new frame
            
            if(Game.IsRunning)
            {
                SpawnCountdown -= (float)Game.UpdateTime.Elapsed.TotalSeconds;
                
                if(SpawnCountdown <= 0)
                {
                    var enempShipInstance = EnemyShipPrefab.Instantiate();
                
                    var enemyShip = enempShipInstance[0];
                    
                    var random = new Random();
    
                    var spawnPosition = new Vector3(random.Next(-3, 3), 2.25f, 0);
                    
                    enemyShip.Transform.Position = spawnPosition;
                    
                    enemyShip.Transform.UpdateWorldMatrix();
    
                    SceneSystem.SceneInstance.RootScene.Entities.Add(enemyShip);
    
                    enemyShip.Get<RigidbodyComponent>().IsKinematic = false;
                    
                    var x = random.Next(-1, 2);
                    
                    enemyShip.Get<RigidbodyComponent>().LinearVelocity = new Vector3((float)x, -1f, 0);
                    
                    SpawnCountdown = SpawnInterval;
                }
            }
        }
    }
}
