// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Engine;
using Xenko.Physics;

namespace HeroWars
{
    public class PlayerCollisionScript : AsyncScript
    {
        public override async Task Execute()
        {
            if (Game.IsRunning)
            {
                var characterComponent = Entity.Get<CharacterComponent>();

                var playerScript = Entity.Get<PlayerScript>();

                while (Game.IsRunning)
                {
                    await Script.NextFrame();

                    var collision = await characterComponent.NewCollision();

                    if (collision.ColliderA.Entity.Name == "EnemyBullet")
                    {
                        var script = collision.ColliderA.Entity.Get<EnemyBulletScript>();

                        script.Die();

                        playerScript.TakeDamage();
                    }
                    else if (collision.ColliderB.Entity.Name == "EnemyBullet")
                    {
                        var script = collision.ColliderB.Entity.Get<EnemyBulletScript>();

                        script.Die();

                        playerScript.TakeDamage();
                    }
                    
                    if(collision.ColliderA.Entity.Name == "HealthUp")
                    {
                        var script = collision.ColliderA.Entity.Get<HealthUpScript>();

                        script.Die();
                        
                        playerScript.AddHealth(1);
                    }
                    
                    if(collision.ColliderB.Entity.Name == "HealthUp")
                    {
                        var script = collision.ColliderB.Entity.Get<HealthUpScript>();

                        script.Die();
                        
                        playerScript.AddHealth(1);
                    }
                }
            }
        }
    }
}
