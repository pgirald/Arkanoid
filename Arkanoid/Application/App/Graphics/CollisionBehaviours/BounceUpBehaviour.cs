using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;

namespace Arkanoid.Application.App.Graphics.CollisionBehaviours
{
    public class BounceUpBehaviour : SpecialBehaviour<Paddle>
    {
        private Projectile _projectile;

        public BounceUpBehaviour(Projectile projectile)
        {
            _projectile = projectile;
        }

        public override void OnCollision(Paddle component, CollisionInfo info)
        {
            if (_projectile.Resting)
            {
                return;
            }
            _projectile.AbsoluteBottom = component.AbsoluteTop;
            _projectile.ChangeVerticalDirection();
        }
    }
}
