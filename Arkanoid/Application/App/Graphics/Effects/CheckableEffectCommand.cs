using Arkanoid.Application.Utils.Game;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Effects
{
    public abstract class CheckableEffectCommand : EffectCommand
    {
        protected CheckableEffectCommand(ArkanoidScenario scenario) : base(scenario)
        {
        }

        private LinkedListNode<CheckableEffectCommand> _key;

        public override void Enable()
        {
            _key = _scenario.ApplyiedCheckableEffects.AddLast(this);
            base.Enable();
        }

        public override void Disable()
        {
            _scenario.ApplyiedCheckableEffects.Remove(_key);
            _key = null;
            base.Disable();
        }

        public abstract void Check(GameInfo info);
    }
}
