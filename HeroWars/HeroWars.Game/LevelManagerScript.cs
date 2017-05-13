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
using SiliconStudio.Xenko.Engine.Events;
using SiliconStudio.Core.Extensions;
using SiliconStudio.Core.Serialization.Contents;

namespace HeroWars
{
    public class LevelManagerScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio

        private EventReceiver GameOverListener = new EventReceiver(GameGlobals.GameOverEventKey);
        private EventReceiver PlayerDeathListener = new EventReceiver(GameGlobals.PlayerDeathEventKey);

        public float SpawnInterval { get; set; }
        public float SpawnCountdown { get; set; }

        private Prefab EnemyShipPrefab;
        private Prefab PlayerPrefab;

        private Entity PlayerEntity;

        public override void Start()
        {
            // Initialization of the script.

            PlayerPrefab = Content.Load<Prefab>("Player");
            EnemyShipPrefab = Content.Load<Prefab>("EnemyShip");

            SpawnInterval = 1f;

            SpawnCountdown = 0;

            Initialize();
        }

        public void Initialize()
        {
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            if (!SceneSystem.SceneInstance.RootScene.Entities.Any(e => e.Name == "Player"))
            {
                var playerInstance = PlayerPrefab.Instantiate();

                PlayerEntity = playerInstance[0];

                var spawnPosition = new Vector3(0, -2, 0);

                PlayerEntity.Transform.Position = spawnPosition;

                PlayerEntity.Transform.UpdateWorldMatrix();

                SceneSystem.SceneInstance.RootScene.Entities.Add(PlayerEntity);
            }
        }

        public override void Update()
        {
            if (PlayerDeathListener.TryReceive())
            {
                GameGlobals.GameOverEventKey.Broadcast();
            }

            if (GameOverListener.TryReceive())
            {
                Content.Unload(SceneSystem.SceneInstance.RootScene);

                SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("GameOver");
            }

            if (Game.IsRunning)
            {
                SpawnCountdown -= (float)Game.UpdateTime.Elapsed.TotalSeconds;

                if (SpawnCountdown <= 0)
                {
                    var enempShipInstance = EnemyShipPrefab.Instantiate();

                    var enemyShip = enempShipInstance[0];

                    var random = new Random();

                    var spawnPosition = new Vector3(random.Next(-3, 3), 2.25f, 0);

                    enemyShip.Transform.Position = spawnPosition;

                    enemyShip.Transform.UpdateWorldMatrix();

                    SceneSystem.SceneInstance.RootScene.Entities.Add(enemyShip);

                    enemyShip.Get<RigidbodyComponent>().IsKinematic = false;

                    var x = random.Next(-1, 2);

                    enemyShip.Get<RigidbodyComponent>().LinearVelocity = new Vector3((float)x, -1f, 0);

                    SpawnCountdown = SpawnInterval;
                }
            }
        }
    }
}
