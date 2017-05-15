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

namespace HeroWars
{
    public class HealthUpScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio

        public override void Start()
        {
            // Initialization of the script.
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
