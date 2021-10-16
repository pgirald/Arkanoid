using Arkanoid.Application.App.Graphics.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App.Graphics.CollisionBehaviours
{
    public class ApplyCheckableEffectBehaviour : ApplyEffectBehaviour
    {
        public ApplyCheckableEffectBehaviour(EffectItem effectItem, ArkanoidScenario scenario) : base(effectItem, scenario)
        {
        }

        public override void ApplyEffect()
        {
            _scenario.ApplyCeckableEffect((CheckableCommandEffect)_effectItem.Effect);
        }
    }
}
