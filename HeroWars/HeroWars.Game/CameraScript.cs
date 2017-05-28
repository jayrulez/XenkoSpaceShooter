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
using SiliconStudio.Xenko.Engine.Events;

namespace HeroWars
{
    public class CameraScript : SyncScript
    {
        private EventReceiver PlayerDeathListener = new EventReceiver(GameGlobals.PlayerDeathEventKey);
        private Vector3 OriginalCameraPosition {get; set;}
        
        public float ShakeDuration;

        public override void Start()
        {
            ShakeDuration = 2;
            OriginalCameraPosition = new Vector3(Entity.Transform.Position.X, Entity.Transform.Position.Y, Entity.Transform.Position.Z);
        }

        public override void Update()
        {
            if(ShakeDuration > 0)
            {
                ShakeDuration -= Game.UpdateTime.Elapsed.Seconds;
                
                var random = new Random();
                
                Entity.Transform.Position = new Vector3(Entity.Transform.Position.X + 0.0005f * (float)random.Next(-1, 2), Entity.Transform.Position.Y + 0.0005f * (float)random.Next(-1, 2), 0);
            }else{
                if(!Vector3.Equals(Entity.Transform.Position, OriginalCameraPosition))
                {
                    Entity.Transform.Position = OriginalCameraPosition;
                }
            }
        }
    }
}
