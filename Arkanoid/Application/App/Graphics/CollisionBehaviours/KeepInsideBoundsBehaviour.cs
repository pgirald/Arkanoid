using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;

namespace Arkanoid.Application.App.Graphics.CollisionBehaviours
{
    public class KeepInsideBoundsBehaviour : SpecialBehaviour<Container>
    {
        private Projectile _projectile;

        public KeepInsideBoundsBehaviour(Projectile projectile)
        {
            _projectile = projectile;
        }

        public override void OnCollision(Container component, CollisionInfo info)
        {
            Side side = info.Side;
            switch (side)
            {
                case Side.Left:
                    _projectile.AbsoluteLeft = info.CollisionSidePosition;
                    _projectile.ChangeHorizontalDirection();
                    break;
                case Side.Right:
                    _projectile.AbsoluteRight = info.CollisionSidePosition;
                    _projectile.ChangeHorizontalDirection();
                    break;
                case Side.Top:
                    _projectile.AbsoluteTop = info.CollisionSidePosition;
                    _projectile.ChangeVerticalDirection();
                    break;
                case Side.Bottom:
                    _projectile.Destroyed?.Invoke(this, null);
                    break;
            }
        }
    }
}
