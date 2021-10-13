using Arkanoid.Application.App.Components.Textures.Messages;
using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class GameOver : IScenarioState
    {
        private GameOverMessage GameOverMessage;

        public GameOver(ScenarioTemplate scenario)
        {
            GameOverMessage = TexturesFactory.GetTexture<GameOverMessage>().Clone<GameOverMessage>();
            GameOverMessage.Align(scenario, Alignment.MiddleCenter);
        }

        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            KeyboardState state = AppControls.State;
            if (state.IsKeyDown(AppControls.Action))
            {
                //Does not work
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
        }
    }
}
