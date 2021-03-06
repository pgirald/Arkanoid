using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.GeneralExtensions;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public class Running : IScenarioState
    {
        private bool pause = false;

        public void SetateRun(ScenarioTemplate scenario, GameInfo info)
        {
            if (info.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P))
            {
                pause = true;
            }
            if (pause)
            {
                return;
            }
            scenario.AnimatedComponents.ForAll(animatedComp =>
            {
                IAnimated animated = (IAnimated)animatedComp.Value;
                info.ComputedSpeed = (float)(info.ElapsedFrameTime * animated.Speed);
                animated.Move(info);
            });
            ((ArkanoidScenario)scenario).ApplyiedCheckableEffects.ForAll(effect => effect.Value.Check(info));
            scenario.CheckForCollisions();
        }

        public IEnumerable<TextureComponent> StateTextures(ScenarioTemplate scenario)
        {
            if (pause)
            {
                yield break;
            }
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
