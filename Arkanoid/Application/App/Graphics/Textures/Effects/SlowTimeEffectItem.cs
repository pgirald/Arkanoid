using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Effects.Concrete;

namespace Arkanoid.Application.App.Graphics.Textures.Effects
{
    public class SlowTimeEffectItem : EffectItem
    {
        public override string TexturePath => "Slower";

        protected override EffectCommand CreateEffect(ArkanoidScenarioUtils utils)
        {
            return new SlowTimeEffect((ArkanoidScenario)utils.Scenario);
        }
    }
}
