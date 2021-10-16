using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Messages;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Game.Scenarios.ScenarioStates
{
    public class GameStart : IScenarioState
    {
        private TitleMessage _title;
        private PressEnterMessage _enterMessage;
        ScenarioTemplate _scenario;

        public GameStart(ScenarioTemplate scenario)
        {
            _scenario = scenario;
            _title = TexturesFactory.GetTextureClone<TitleMessage>();
            _enterMessage = TexturesFactory.GetTextureClone<PressEnterMessage>();
            _title.Align(scenario, Alignment.TopCenter);
            _title.AbsoluteTop += 40;
            _enterMessage.PutOn(_title, Alignment.BottomCenter);
            _enterMessage.AbsoluteTop += 40;
        }

        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            if (info.KeyboardState.IsKeyDown(AppControls.Enter))
            {
                _scenario.CurrentState = new Running();
                ((ArkanoidScenario)scenario).InitialState = _scenario.CurrentState;
            }
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            yield return _title;
            yield return _enterMessage;
        }
    }
}
