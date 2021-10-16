using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Effects.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App.Graphics.Textures.Effects
{
    public class StickyEffectItem : EffectItem
    {
        public override string TexturePath => "Sticky";

        protected override EffectCommand CreateEffect(ArkanoidScenarioUtils utils)
        {
            return new StickyEffectCommand(utils);
        }
    }
}
