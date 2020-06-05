// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Animations;

namespace HeroWars
{
    public class EnemyExplosionScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        private SpriteComponent SpriteComponent;

        public override void Start()
        {
            // Initialization of the script.
            SpriteComponent = Entity.Get<SpriteComponent>();
            
            SpriteAnimation.Play(SpriteComponent, 0, SpriteComponent.SpriteProvider.SpritesCount -1, AnimationRepeatMode.PlayOnce);
        }

        public override void Update()
        {
            // Do stuff every new frame
            
            if(SpriteComponent.CurrentFrame == SpriteComponent.SpriteProvider.SpritesCount -1)
            {
                SceneSystem.SceneInstance.RootScene.Entities.Remove(Entity);
            }
        }
    }
}
