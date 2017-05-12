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
    public class PlayerScript : SyncScript
    {
        private Prefab LaserShotPrefab;

        private int HitPoints = 10;
        
        [Flags]
        private enum InputState
        {
            None = 0x0,
            Up = 0x2,
            Down = 0x4,
            Left = 0x8,
            Right = 0x16
        }

        private InputState OldInputState = InputState.None;

        private Vector3 StartPosition { get; set; }

        private Vector3 Velocity;
        private Vector3 Position;
        private float Speed;

        private float ScreenWidth;
        private float ScreenHeight;
        
        float ReloadTime;
        float ReloadCountdown;

        public override void Start()
        {
            // Initialization of the script.

            StartPosition = Entity.Transform.Position;

            ScreenWidth = GraphicsDevice.Presenter.BackBuffer.Width;
            ScreenHeight = GraphicsDevice.Presenter.BackBuffer.Height;
            
            LaserShotPrefab = Content.Load<Prefab>("PlayerBullet");
            
            ReloadTime = 0.17f;
            ReloadCountdown = 0;

            Initialize();
        }

        public override void Update()
        {
            if (Game.IsRunning)
            {
                UpdateTransform();
                
                ReloadCountdown -= (float)Game.UpdateTime.Elapsed.TotalSeconds;
                
                if (ReloadCountdown <= 0)
                {
                    FireShot();
                    ReloadCountdown = ReloadTime;
                }
            }
        }

        private void Initialize()
        {
            Position = Vector3.Zero;
            Velocity = Vector3.Zero;

            Speed = 5;
        }

        private void UpdateTransform()
        {
            var inputState = GetInputState();

            float angle = 0f;

            if (inputState == InputState.Left)
            {
                angle = (float)Math.PI;
            }

            if (inputState == InputState.Up)
            {
                angle = (float)Math.PI / 2;
            }

            if (inputState == InputState.Right)
            {
                angle = 0f;
            }

            if (inputState == InputState.Down)
            {
                angle = 3.0f * (float)Math.PI / 2;
            }

            if (inputState == InputState.None)
            {
                Velocity = Vector3.Zero;
                //Position = Vector3.Zero;
            }
            else
            {
                Position = Velocity = new Vector3((float)Math.Cos(angle) * Speed, (float)Math.Sin(angle) * Speed, 0f);
            }

            Entity.Get<CharacterComponent>().SetVelocity(Velocity);

            //Console.WriteLine(Velocity);
            //Console.WriteLine(SceneSystem.SceneInstance.RootScene.Entities.Count);

            //Entity.Transform.Position = Position;
        }
        
        private void FireShot()
        {
            if(Input.IsKeyDown(Keys.Space))
            {
                var bulletInstance = LaserShotPrefab.Instantiate();
                
                var bullet = bulletInstance[0];

                var playerSprite = Entity.Get<SpriteComponent>();

                var spawnPosition = new Vector3(Entity.Transform.Position.X, Entity.Transform.Position.Y + 0.35f, Entity.Transform.Position.Z);
                
                bullet.Transform.Position = spawnPosition;
                
                bullet.Transform.UpdateWorldMatrix();

                SceneSystem.SceneInstance.RootScene.Entities.Add(bullet);

                bullet.Get<RigidbodyComponent>().IsKinematic = false;
                //bullet.Get<RigidbodyComponent>().Activate();
                //bullet.Get<RigidbodyComponent>().LinearFactor = new Vector3(0, 1, 0);
                //bullet.Get<RigidbodyComponent>().AngularFactor = new Vector3(0, 0, 0);
                //bullet.Get<RigidbodyComponent>().ApplyImpulse(new Vector3(0, 25, 0));
                bullet.Get<RigidbodyComponent>().LinearVelocity = new Vector3(0, 20, 0);
            }
        }

        private InputState GetInputState()
        {
            InputState inputState = InputState.None;

            if (Input.IsKeyDown(Keys.Left))
            {
                inputState = InputState.Left;
            }

            if (Input.IsKeyDown(Keys.Right))
            {
                inputState = InputState.Right;
            }

            if (Input.IsKeyDown(Keys.Up))
            {
                inputState = InputState.Up;
            }

            if (Input.IsKeyDown(Keys.Down))
            {
                inputState = InputState.Down;
            }

            return inputState;
        }

        public void Die()
        {
        }

        public void TakeDamage()
        {
            HitPoints--;

            if(HitPoints <= 0)
            {
                SceneSystem.SceneInstance.RootScene.Entities.Remove(Entity);

                Cancel();
            }
        }
    }
}
