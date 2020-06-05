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
using Stride.Physics;
using Stride.Engine.Events;
using Stride.Core.Extensions;
using Stride.Core.Serialization.Contents;
using Stride.UI.Controls;
using Stride.UI;
using Stride.Audio;
using Stride.UI.Events;

namespace HeroWars
{
    public class LevelManagerScript : SyncScript
    {
        // Declared public member fields and properties will show in the game studio

        private EventReceiver GameOverListener = new EventReceiver(GameGlobals.GameOverEventKey);
        private EventReceiver PlayerDeathListener = new EventReceiver(GameGlobals.PlayerDeathEventKey);
        private EventReceiver EnemyDeathListener = new EventReceiver(GameGlobals.EnemyDeathEventKey);

        private Vector3 OriginalCameraPosition;
        
        public Sound BackgroundMusic {get; set;}
        public SoundInstance BackgroundMusicInstance {get; set;}

        public float EnemySpawnInterval { get; set; }
        public float EnemySpawnCountdown { get; set; }

        public float HealthUpSpawnInterval { get; set; }
        public float HealthUpSpawnCountdown { get; set; }

        private Prefab EnemyShipPrefab;
        private Prefab PlayerPrefab;
        private Prefab HealthUpPrefab;

        private Entity PlayerEntity;
        
        public UIPage GameHUD {get; set;}
        
        private TextBlock ScoreText {get; set; }
        private TextBlock HighScoreText { get; set; }
        private TextBlock HealthText {get; set;}
        
        private int Score {get; set;}
        private int CurrentHighScore;
        private Entity Camera;

        public override void Start()
        {
            // Initialization of the script.

            PlayerPrefab = Content.Load<Prefab>("Player");
            EnemyShipPrefab = Content.Load<Prefab>("EnemyShip");
            HealthUpPrefab = Content.Load<Prefab>("HealthUp");

            EnemySpawnInterval = 1f;
            EnemySpawnCountdown = 3f;

            HealthUpSpawnInterval = 10f;
            HealthUpSpawnCountdown = HealthUpSpawnInterval;

            ScoreText = GameHUD?.RootElement.FindVisualChildOfType<TextBlock>("ScoreText");
            HealthText = GameHUD?.RootElement.FindVisualChildOfType<TextBlock>("HealthText");
            HighScoreText = GameHUD?.RootElement.FindVisualChildOfType<TextBlock>("HighScoreText");

            BackgroundMusicInstance = BackgroundMusic.CreateInstance();

            //OriginalCameraPosition = 

            Initialize();
        }

        public void Initialize()
        {
        
            BackgroundMusicInstance.Volume = 0.25f;
            BackgroundMusicInstance.Stop();
            BackgroundMusicInstance.IsLooping = true;
            BackgroundMusicInstance.Play();
            Score = 0;
            SpawnPlayer();
            CurrentHighScore = GameGlobals.GetHighScore();
            UpdateHighScoreText(CurrentHighScore);
        }

        private void UpdateHighScoreText(int score)
        {
            HighScoreText.Text = $"High Score: {score}";
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

        public void SpawnEnemy()
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
        }
        
        public void SpawnHealthUp()
        {
            var healthUpInstance = HealthUpPrefab.Instantiate()[0];
            
            var random = new Random();

            var spawnPosition = new Vector3(random.Next(-3, 3), 2.25f, 0);

            healthUpInstance.Transform.Position = spawnPosition;

            healthUpInstance.Transform.UpdateWorldMatrix();

            SceneSystem.SceneInstance.RootScene.Entities.Add(healthUpInstance);

            healthUpInstance.Get<RigidbodyComponent>().IsKinematic = false;

            var x = random.Next(-1, 2);

            healthUpInstance.Get<RigidbodyComponent>().LinearVelocity = new Vector3(0, -0.5f, 0);
        }

        public override void Update()
        {
            if (PlayerDeathListener.TryReceive())
            {
                GameGlobals.GameOverEventKey.Broadcast();
            }

            if (GameOverListener.TryReceive())
            {
                if(Score > CurrentHighScore)
                {
                    GameGlobals.SetHighScore(Score);
                }

                BackgroundMusicInstance.Stop();
            
                Content.Unload(SceneSystem.SceneInstance.RootScene);

                SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("GameOver");
                
                //GameOverText.Visibility = Visibility.Visible;
            }

            if(EnemyDeathListener.TryReceive())
            {
                Score++;
            }

            if (Game.IsRunning)
            {
                ScoreText.Text = $"Score: {Score}";
                HealthText.Text = $"Health: {PlayerEntity.Get<PlayerScript>().HitPoints}";

                if(Score > CurrentHighScore)
                {
                    UpdateHighScoreText(Score);
                }
            
                EnemySpawnCountdown -= (float)Game.UpdateTime.Elapsed.TotalSeconds;

                if (EnemySpawnCountdown <= 0)
                {
                    SpawnEnemy();

                    EnemySpawnCountdown = EnemySpawnInterval;
                }

                HealthUpSpawnCountdown -= (float)Game.UpdateTime.Elapsed.TotalSeconds;

                if (HealthUpSpawnCountdown <= 0)
                {
                    SpawnHealthUp();

                    HealthUpSpawnCountdown = HealthUpSpawnInterval;
                }
            }else{
            }
        }
    }
}
