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

namespace HeroWars
{
    public class EnemyBulletScript : SyncScript
    {
        public override void Start()
        {
        }

        public override void Update()
        {
            if (Entity.Transform.Position.Y <= -2.25)
            {
                Die();
            }
        }
        
        public void Die()
        {
            SceneSystem.SceneInstance.RootScene.Entities.Remove(Entity);
        }
    }
}
