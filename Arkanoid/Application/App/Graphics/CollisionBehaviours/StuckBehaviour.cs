using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;

namespace Arkanoid.Application.App.Graphics.CollisionBehaviours
{
    public class StuckBehaviour : SpecialBehaviour<Paddle>
    {
        private StickyProjectile _projectile;

        public StuckBehaviour(StickyProjectile projectile)
        {
            _projectile = projectile;
        }

        public override void OnCollision(Paddle component, CollisionInfo info)
        {
            if (_projectile.AbsoluteRight > component.AbsoluteRight)
            {
                _projectile.AbsoluteRight = component.AbsoluteRight;
            }
            if (_projectile.AbsoluteLeft < component.AbsoluteLeft)
            {
                _projectile.AbsoluteLeft = component.AbsoluteLeft;
            }
            _projectile.AbsoluteBottom = component.AbsoluteTop - 0.1f;
            _projectile.Resting = true;
            _projectile.ChangeVerticalDirection();
        }
    }
}
