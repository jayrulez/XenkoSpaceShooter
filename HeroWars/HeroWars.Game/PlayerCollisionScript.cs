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
    public class PlayerCollisionScript : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override async Task Execute()
        {
            var characterComponent = Entity.Get<CharacterComponent>();
            
            var playerScript = Entity.Get<PlayerScript>();
            
            while(Game.IsRunning)
            {
                // Do stuff every new frame
                await Script.NextFrame();
                
                var collision = await characterComponent.NewCollision();

                if(collision.ColliderA.Entity.Name == "EnemyBullet")
                {
                    var script = collision.ColliderA.Entity.Get<EnemyBulletScript>();

                    script.Die();

                    playerScript.TakeDamage();
                }else if (collision.ColliderB.Entity.Name == "EnemyBullet")
                {
                    var script = collision.ColliderB.Entity.Get<EnemyBulletScript>();

                    script.Die();

                    playerScript.TakeDamage();
                }
            }
        }
    }
}
