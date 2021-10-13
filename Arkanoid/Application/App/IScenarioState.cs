using Arkanoid.Application.Utils;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public interface IScenarioState
    {
        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario);

        public void SetateRun(ScenarioTemplate scenario, GameInfo info);
    }
}
