using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class CommandEffect
    {
        public CommandEffect(ArkanoidScenario scenario)
        {
            _scenario = scenario;
            IncompatibleEffects = new Dictionary<string, CommandEffect>();
        }

        protected Dictionary<string, CommandEffect> IncompatibleEffects;

        protected ArkanoidScenario _scenario;

        public abstract void Enable();

        public abstract void Disable();
    }
}
