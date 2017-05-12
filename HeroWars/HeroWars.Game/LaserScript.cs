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
    public class LaserScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override void Start()
        {
            
        }

        public override void Update()
        {
            // Do stuff every new frame
            
            if (Entity.Transform.Position.Y >= 5)
            {
                Die();
            }
        }
        
        public void Die()
        {
            SceneSystem.SceneInstance.RootScene.Entities.Remove(Entity);
            
            Cancel();
        }
    }
}
