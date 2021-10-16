using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Effects.Concrete;

namespace Arkanoid.Application.App.Graphics.Textures.Effects
{
    public class BulletItemEffect : EffectItem
    {
        public override string TexturePath => "Bullet";

        protected override EffectCommand CreateEffect(ArkanoidScenarioUtils utils)
        {
            return new GunPaddleEffect((ArkanoidScenario)utils.Scenario);
        }
    }
}
