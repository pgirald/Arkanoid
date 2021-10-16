using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Messages;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Game.Scenarios.ScenarioStates
{
    public class NextStage : IScenarioState
    {
        private PressEnterMessage _enterMessage;
        private NextArrowMessage _arrowMessage;

        public EventHandler NextStageConfirmed;

        public NextStage(ScenarioTemplate scenario)
        {
            _arrowMessage = TexturesFactory.GetTextureClone<NextArrowMessage>();
            _enterMessage = TexturesFactory.GetTextureClone<PressEnterMessage>();
            _enterMessage.Align(scenario, Alignment.TopCenter);
            _enterMessage.AbsoluteTop += 80;
            _arrowMessage.PutOn(_enterMessage, Alignment.BottomCenter);
            _arrowMessage.AbsoluteTop += 20;
        }

        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            if (info.KeyboardState.IsKeyDown(AppControls.Enter))
            {
                NextStageConfirmed?.Invoke(this, null);
            }
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            yield return _enterMessage;
            yield return _arrowMessage;
        }
    }
}
