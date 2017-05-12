using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Xenko.Input;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Physics;

namespace HeroWars
{
    public class EnemyShip : SyncScript
    {
        public float ShipSpeed { get; set; }
        
        public float FireInterval {get; set;}
        
        private Prefab BulletPrefab;
        float ReloadTime;
        float ReloadCountdown;

        public override void Start()
        {
            BulletPrefab = Content.Load<Prefab>("EnemyBullet");
            
            ReloadTime = 0.2f;
            ReloadCountdown = 0;
        }
        
        public override void Update()
        {
            if (Game.IsRunning)
            {                
                ReloadCountdown -= (float)Game.UpdateTime.Elapsed.TotalSeconds;
                
                if (ReloadCountdown <= 0)
                {
                    var random = new Random();
                    
                    if(random.Next(0, 2) == 1)
                    {
                        FireShot();
                    }
                    
                    ReloadCountdown = ReloadTime;
                }

                if(Entity.Transform.Position.X < -4f || Entity.Transform.Position.X > 4f || Entity.Transform.Position.Y < -3f)
                {
                    Die();
                }
            }
        }
        
        private void FireShot()
        {
            var bulletInstance = BulletPrefab.Instantiate();
            
            var bullet = bulletInstance[0];

            var playerSprite = Entity.Get<SpriteComponent>();

            var spawnPosition = new Vector3(Entity.Transform.Position.X, Entity.Transform.Position.Y - 0.35f, Entity.Transform.Position.Z);
            
            bullet.Transform.Position = spawnPosition;
            
            bullet.Transform.UpdateWorldMatrix();

            SceneSystem.SceneInstance.RootScene.Entities.Add(bullet);

            bullet.Get<RigidbodyComponent>().IsKinematic = false;
            bullet.Get<RigidbodyComponent>().LinearVelocity = new Vector3(0, -5, 0);
        }

        public void Die()
        {
            SceneSystem.SceneInstance.RootScene.Entities.Remove(Entity);

            Cancel();
        }
    }
}
