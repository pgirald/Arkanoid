using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App.Graphics.Effects.Concrete
{
    public class FastTimeEffect : TimeEffectCommand
    {
        private float _originalSpeed;

        public FastTimeEffect(ArkanoidScenario scenario) : base(scenario)
        {
            AddIncompatibleEffect<SlowTimeEffect>();
        }

        public override float SecondsLimit => 6;

        protected override void ChangeScenario()
        {
            _originalSpeed = _scenario.Projectile.Speed;
            _scenario.Projectile.Speed += 100f;
        }

        protected override void RecoverScenario()
        {
            _scenario.Projectile.Speed = _originalSpeed;
        }
    }
}
