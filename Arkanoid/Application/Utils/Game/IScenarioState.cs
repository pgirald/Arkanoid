using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Game
{
    public interface IScenarioState
    {
        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario);

        public void SetateRun(ScenarioTemplate scenario, GameInfo info);
    }
}
