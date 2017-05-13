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
    public class SpaceBackgroundScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        
        private Vector3 StartPosition;

        public float TileSize { get; set; }
        public float ScrollSpeed { get; set; }
        public int Index { get; set; }

        public override void Start()
        {
            StartPosition = Entity.Transform.Position;
        }
        
        public float Repeat(float t, float length)
		{
			return (t - (float)Math.Floor(t / length) * length);
		}

        public override void Update()
        {
            if(Game.IsRunning)
            {
                var newPosition = this.Repeat((float)Game.UpdateTime.Total.TotalSeconds * ScrollSpeed, TileSize);

                Entity.Transform.Position = StartPosition - Vector3.UnitY * newPosition;
            }
        }
    }
}
