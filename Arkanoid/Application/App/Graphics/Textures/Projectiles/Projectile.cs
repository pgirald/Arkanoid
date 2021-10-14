using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.Utils.Collisions;
using System;

namespace Arkanoid.Application.App.Graphics.Textures.Projectiles
{
    public class Projectile : CollideableComponent, IAnimatedComponent
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

        protected override void Proceed(float trueSpeed)
        {
            if (Resting)
            {
                return;
            }
            X += trueSpeed * _xDirection;
            Y -= trueSpeed * _yDirection;
        }

        public override void OnContainerHit(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    AbsoluteLeft = Container.AbsoluteLeft;
                    _changeHorizontalDirection();
                    break;
                case Side.Right:
                    AbsoluteRight = Container.AbsoluteRight;
                    _changeHorizontalDirection();
                    break;
                case Side.Top:
                    AbsoluteTop = Container.AbsoluteTop;
                    _changeVerticalDirection();
                    break;
                case Side.Bottom:
                    Destroyed?.Invoke(this, null);
                    break;
            }
        }

        public override void OnPaddleHit(Paddle paddle, Side side)
        {
            AbsoluteBottom = paddle.AbsoluteTop;
            _changeVerticalDirection();
        }

        public override void OnBlockHit(Block block, Side side)
        {
            switch (side)
            {
                case Side.Left:
                    AbsoluteLeft = block.AbsoluteRight;
                    _changeHorizontalDirection();
                    break;
                case Side.Right:
                    AbsoluteRight = block.AbsoluteLeft;
                    _changeHorizontalDirection();
                    break;
                case Side.Top:
                    AbsoluteTop = block.AbsoluteBottom;
                    _changeVerticalDirection();
                    break;
                case Side.Bottom:
                    AbsoluteBottom = block.AbsoluteTop;
                    _changeVerticalDirection();
                    break;
            }
        }

        public void FixToLeft()
        {
            _xDirection = -1;
        }

        public void FixToRight()
        {
            _xDirection = 1;
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
