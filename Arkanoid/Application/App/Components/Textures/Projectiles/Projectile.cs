using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.App.Components.Textures.Paddles;
using Arkanoid.Application.Utils;
using System;

namespace Arkanoid.Application.App.Components.Textures.Projectiles
{
    public class Projectile : CollideableComponent, IAnimatedComponent
    {
        public Projectile()
        {
            Speed = 10f;
        }

        private int _xDirection = 1;
        private int _yDirection = 1;

        public EventHandler GameLost;

        public override string ParentPath => "Projectiles/";

        public override string TexturePath => "Projectile";

        protected override void Proceed(float trueSpeed)
        {
            X += trueSpeed * _xDirection;
            Y -= trueSpeed * _yDirection;
        }

        public override void OnContainerHit(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    Left = Container.Left;
                    _changeHorizontalDirection();
                    break;
                case Side.Right:
                    Right = Container.Right;
                    _changeHorizontalDirection();
                    break;
                case Side.Top:
                    Top = Container.Top;
                    _changeVerticalDirection();
                    break;
                case Side.Bottom:
                    Bottom = Container.Bottom;
                    _changeVerticalDirection();
                    break;
            }
        }

        public override void OnPaddleHit(Paddle paddle, Side side)
        {
            AbsoluteTop = paddle.AbsoluteTop;
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
