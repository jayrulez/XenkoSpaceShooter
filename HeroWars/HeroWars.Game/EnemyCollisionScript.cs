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
    public class EnemyCollisionScript : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override async Task Execute()
        {
            var rigidBody = Entity.Get<RigidbodyComponent>();
            
            var enemyScript = Entity.Get<EnemyShip>();
            
            while(Game.IsRunning)
            {
                // Do stuff every new frame
                await Script.NextFrame();
                
                var collision = await rigidBody.NewCollision();

                if(collision.ColliderA.Entity.Name == "PlayerBullet" && rigidBody.IsTrigger)
                {
                    var script = collision.ColliderA.Entity.Get<LaserScript>();

                    script.Die();

                    enemyScript.Die();
                }else if (collision.ColliderB.Entity.Name == "PlayerBullet" && rigidBody.IsTrigger)
                {
                    var script = collision.ColliderB.Entity.Get<LaserScript>();

                    script.Die();

                    enemyScript.Die();
                }
            }
        }
    }
}
