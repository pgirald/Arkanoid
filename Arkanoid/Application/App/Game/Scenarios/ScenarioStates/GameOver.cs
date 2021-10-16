using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Messages;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class GameOver : IScenarioState
    {
        private GameOverMessage GameOverMessage;
        private RMessage rMessage;

        public GameOver(ScenarioTemplate scenario)
        {
            GameOverMessage = TexturesFactory.GetTextureClone<GameOverMessage>();
            rMessage = TexturesFactory.GetTextureClone<RMessage>();
            GameOverMessage.Align(scenario, Alignment.MiddleCenter);
            rMessage.PutOn(GameOverMessage, Alignment.BottomCenter);
            rMessage.AbsoluteTop += 20;
        }

        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            KeyboardState state = AppControls.State;
            if (state.IsKeyDown(AppControls.Restart))
            {
                scenario.Restart();
            }
            else if (state.IsKeyDown(AppControls.Exit))
            {
                scenario.Clear();
            }
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            yield return GameOverMessage;
            yield return rMessage;
        }
    }
}
