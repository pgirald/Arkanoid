using Arkanoid.Application.Utils;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class Running : IScenarioState
    {
        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            foreach (CollideableComponent component in scenario.GetMovingComponents())
            {
                component.Move(info.ElapsedFrameTime);
            }
            scenario.CheckForCollisions();
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            return scenario.GameTextures;
        }
    }
}
