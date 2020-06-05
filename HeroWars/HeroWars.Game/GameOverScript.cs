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
using Stride.UI;
using Stride.UI.Controls;
using Stride.UI.Panels;
using Stride.UI.Events;
using Stride.Core.Serialization.Contents;
using Stride.Games;

namespace HeroWars
{
    public class GameOverScript : SyncScript
    {
        private Button ExitButton;
        private Button RetryButton;
        public UIPage GameOverUi { get; set; }

        public override void Start()
        {
            ExitButton = GameOverUi?.RootElement.FindVisualChildOfType<Button>("ExitButton");
            RetryButton = GameOverUi?.RootElement.FindVisualChildOfType<Button>("RetryButton");

            ExitButton.Click += (object sender, RoutedEventArgs routedEventArgs) =>
            {
                var game = (Game)Game;

                game.Exit();
            };

            RetryButton.Click += (object sender, RoutedEventArgs routedEventArgs) =>
            {
                Content.Unload(SceneSystem.SceneInstance.RootScene);
                SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("MainScene");
            };
        }

        public override void Update()
        {
        }
    }
}
