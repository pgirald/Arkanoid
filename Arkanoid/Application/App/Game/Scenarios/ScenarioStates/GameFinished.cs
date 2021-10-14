using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Messages;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class GameFinished : IScenarioState
    {
        private WinMessage VictoryMessage;
        public GameFinished(ScenarioTemplate scenario)
        {
            VictoryMessage = TexturesFactory.GetTextureClone<WinMessage>();
            VictoryMessage.Align(scenario, Alignment.MiddleCenter);
        }

        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            yield return VictoryMessage;
        }
    }
}
