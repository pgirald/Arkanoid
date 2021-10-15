using Arkanoid.Application.App.Graphics.Enums;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using System;

namespace Arkanoid.Application.App.Graphics.Textures.Projectiles
{
    public class Projectile : AnimatedTexture
    {
        public Projectile()
        {
            Speed = 300f;
        }

        public EventHandler RestStart;

        private int _xDirection = 1;
        private int _yDirection = 1;
        private bool _resting = true;

        public bool Resting
        {
            get => _resting;
            set
            {
                _resting = value;
                if (_resting)
                {
                    RestStart?.Invoke(this, null);
                }
            }
        }

        public override string ParentPath => "Projectiles/";

        public override string TexturePath => "Projectile";

        public override void Move(float computedSpeed)
        {
            if (Resting)
            {
                return;
            }
            X += computedSpeed * _xDirection;
            Y -= computedSpeed * _yDirection;
        }

        public override void OnCollision(Component collideable, CollisionInfo info)
        {
            Side side = info.Side;
            switch (side)
            {
                case Side.Left:
                    AbsoluteRight = info.CollisionSidePosition;
                    ChangeHorizontalDirection();
                    break;
                case Side.Right:
                    AbsoluteLeft = info.CollisionSidePosition;
                    ChangeHorizontalDirection();
                    break;
                case Side.Top:
                    AbsoluteBottom = info.CollisionSidePosition;
                    ChangeVerticalDirection();
                    break;
                case Side.Bottom:
                    AbsoluteTop = info.CollisionSidePosition;
                    ChangeVerticalDirection();
                    break;
            }
        }

        public void FixTo(ProjectileDirection direction)
        {
            _xDirection = (int)direction;
        }

        public void ChangeHorizontalDirection()
        {
            _xDirection *= -1;
        }

        public void ChangeVerticalDirection()
        {
            _yDirection *= -1;
        }

    }
}
