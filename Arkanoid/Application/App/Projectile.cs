using Arkanoid.Application.Utils;

namespace Arkanoid.Application.App
{
    public class Projectile : AnimatedTexture
    {
        private int _xDirection = 1;
        private int _yDirection = 1;

        public override string ParentPath => "Projectiles/";

        public override string TexturePath => "Projectile";

        public Projectile()
        {
            Speed = 3f;
        }

        public void move()
        {
            X += Speed * _xDirection;
            Y -= Speed * _yDirection;
            _bounceIfColise();
        }

        private void _bounceIfColise()
        {
            if (Right > Container.Right)
            {
                Right = Container.Right;
                _changeHorizontalDirection();
            }
            if (Left < Container.Left)
            {
                Left = Container.Left;
                _changeHorizontalDirection();
            }
            if (Top < Container.Top)
            {
                Top = Container.Top;
                _changeVerticalDirection();
            }
            if (Bottom > Container.Bottom)
            {
                Bottom = Container.Bottom;
                _changeVerticalDirection();
            }
        }

        public void _changeHorizontalDirection()
        {
            _xDirection *= -1;
        }

        public void _changeVerticalDirection()
        {
            _yDirection *= -1;
        }
    }
}
