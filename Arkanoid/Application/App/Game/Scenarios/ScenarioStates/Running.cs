using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
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
