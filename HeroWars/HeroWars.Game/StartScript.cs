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
using SiliconStudio.Xenko.UI;
using SiliconStudio.Xenko.UI.Controls;
using SiliconStudio.Xenko.UI.Panels;

namespace HeroWars
{
    public class StartScript : SyncScript
    {
        private enum TextEffectMode
        {
            Brighten,
            Lighten
        }
        // Declared public member fields and properties will show in the game studio

        public UIPage StartUI { get; set; }

        private TextBlock StartText;

        private TextEffectMode StartTextEffectMode { get; set; }

        public override void Start()
        {
            // Initialization of the script.

            StartText = StartUI?.RootElement.FindVisualChildOfType<TextBlock>("StartText");

            StartTextEffectMode = TextEffectMode.Lighten;

            StartText.Opacity = 1f;
        }

        public override void Update()
        {
            // Do stuff every new frame

            if (StartTextEffectMode == TextEffectMode.Lighten)
            {
                if (StartText.Opacity <= 0.1f)
                {
                    StartTextEffectMode = TextEffectMode.Brighten;
                }
                else
                {
                    StartText.Opacity -= 0.005f;
                }
            }

            if (StartTextEffectMode == TextEffectMode.Brighten)
            {
                if (StartText.Opacity >= 1f)
                {
                    StartTextEffectMode = TextEffectMode.Lighten;
                }
                else
                {
                    StartText.Opacity += 0.005f;
                }
            }

            if (Input.PointerEvents.Any(e => e.State == PointerState.Down))
            {
                Content.Unload(SceneSystem.SceneInstance.RootScene);
                SceneSystem.SceneInstance.RootScene = Content.Load<Scene>("MainScene");
            }
        }
    }
}
