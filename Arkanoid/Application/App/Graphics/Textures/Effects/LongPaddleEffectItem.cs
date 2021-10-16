using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Effects.Concrete;

namespace Arkanoid.Application.App.Graphics.Textures.Effects
{
    public class LongPaddleEffectItem : EffectItem
    {
        public override string TexturePath => "Long";

        protected override EffectCommand CreateEffect(ArkanoidScenarioUtils utils)
        {
            return new LongPaddleEffect((ArkanoidScenario)utils.Scenario);
        }
    }
}
