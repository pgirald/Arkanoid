using Arkanoid.Application.App.Graphics.Effects;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.Utils.Collisions;

namespace Arkanoid.Application.App.Graphics.CollisionBehaviours
{
    public class ApplyEffectBehaviour : SpecialBehaviour<Paddle>
    {
        public ApplyEffectBehaviour(EffectItem effectItem)
        {
            _effectItem = effectItem;
        }

        protected EffectItem _effectItem;

        public override void OnCollision(Paddle component, CollisionInfo info)
        {
            _effectItem.Destroyed?.Invoke(_effectItem, null);
            _effectItem.Effect.Enable();
        }
    }
}
