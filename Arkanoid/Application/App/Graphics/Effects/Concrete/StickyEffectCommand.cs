using Arkanoid.Application.App.Game.Scenarios;
using Arkanoid.Application.App.Graphics.CollisionBehaviours;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;

namespace Arkanoid.Application.App.Graphics.Effects.Concrete
{
    public class StickyEffectCommand : TimeEffectCommand
    {
        private ArkanoidScenarioUtils _utils;

        public StickyEffectCommand(ArkanoidScenarioUtils utils) : base((ArkanoidScenario)utils.Scenario)
        {
            _utils = utils;
        }

        public override float SecondsLimit => 8;

        protected override void ChangeScenario()
        {
            StickyProjectile sticky = TexturesFactory.GetTextureClone<StickyProjectile>();
            _scenario.SetProjectile(sticky);
            sticky.CMKey.Value.SpecialBehaviours.Clear();
            _utils.CM.AddSpecial(sticky, _scenario.Paddle, new StuckBehaviour(sticky));
            _utils.CM.AddSpecial(sticky, _scenario, new KeepInsideBoundsBehaviour(sticky));
        }

        protected override void RecoverScenario()
        {
            Projectile projectile = TexturesFactory.GetTextureClone<Projectile>();
            projectile.Resting = false;
            _scenario.SetProjectile(projectile);
            projectile.CMKey.Value.SpecialBehaviours.Clear();
            _utils.CM.AddSpecial(projectile, _scenario.Paddle, new BounceUpBehaviour(projectile));
            _utils.CM.AddSpecial(projectile, _scenario, new KeepInsideBoundsBehaviour(projectile));
        }
    }
}
