using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.Utils.Collisions;

namespace Arkanoid.Application.App.Graphics.CollisionBehaviours
{
    public abstract class ApplyEffectBehaviour : SpecialBehaviour<Paddle>
    {
        public ApplyEffectBehaviour(EffectItem effectItem, ArkanoidScenario scenario)
        {
            _effectItem = effectItem;
            _scenario = scenario;
        }

        protected EffectItem _effectItem;
        protected ArkanoidScenario _scenario;

        public abstract void ApplyEffect();

        public override void OnCollision(Paddle component, CollisionInfo info)
        {
            _effectItem.Destroyed?.Invoke(_effectItem, null);
            ApplyEffect();
        }
    }
}
