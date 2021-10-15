using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.GeneralExtensions;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class Running : IScenarioState
    {
        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            scenario.AnimatedComponents.ForAll(animatedComp =>
            {
                IAnimated animated = (IAnimated)animatedComp.Value;
                info.ComputedSpeed = info.ElapsedFrameTime * animated.Speed;
                animated.Move(info);
            });
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
