using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class Running : IScenarioState
    {
        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            foreach (IAnimated animated in scenario.GetAnimatedComponents())
            {
                animated.Move(info.ElapsedFrameTime * animated.Speed);
            }
            scenario.CheckForCollisions();
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            foreach (IDrawable drawable in scenario.GameTextures)
            {
                foreach (TextureComponent texture in drawable.Textures)
                {
                    yield return texture;
                }
            }
        }
    }
}
