using Arkanoid.Application.App.Components.Textures.Messages;
using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class GameFinished : IScenarioState
    {
        private WinMessage VictoryMessage;
        public GameFinished(ScenarioTemplate scenario)
        {
            VictoryMessage = TexturesFactory.GetTexture<WinMessage>();
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
