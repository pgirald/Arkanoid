using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arkanoid.Application.App.Graphics.Textures.Paddles
{
    public class Paddle : CollideableComponent, IAnimatedComponent
    {
        public Paddle()
        {
            Speed = 500f;
        }

        private float LeftBound;
        private float RightBound;
        private Projectile _projectile;

        protected override void Proceed(float trueSpeed)
        {
            KeyboardState state = AppControls.State;
            if (state.IsKeyDown(AppControls.Right))
            {
                X += trueSpeed;
                if (_projectile.Resting)
                {
                    _projectile.FixToRight();
                    _projectile.X += trueSpeed;
                }
            }
            if (state.IsKeyDown(AppControls.Left))
            {
                X -= trueSpeed;
                if (_projectile.Resting)
                {
                    _projectile.FixToLeft();
                    _projectile.X -= trueSpeed;
                }
            }
            if (_projectile.Resting)
            {
                KeepProjectileInBounds();
            }
            if (state.IsKeyDown(AppControls.Action))
            {
                _projectile.Resting = false;
            }
        }

        private void OnProjetileRestStart(object sender, EventArgs args)
        {
            ResetProjectileBounds();
        }

        public void ResetProjectileBounds()
        {
            LeftBound = ProjectileLeftBound;
            RightBound = ProjectileRightBound;
        }

        public Projectile Projectile
        {
            get => _projectile;
            set
            {
                if (_projectile != null)
                {
                    _projectile.RestStart -= OnProjetileRestStart;
                }
                if (Container != value.Container)
                {
                    throw new Exception("This paddle and the specified projectiles have different containers");
                }
                _projectile = value;
                _projectile.RestStart += OnProjetileRestStart;
                ResetProjectileBounds();
            }
        }

        public override string ParentPath => "Paddles/";

        public override string TexturePath => "Paddle";

        public override void OnContainerHit(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    AbsoluteLeft = Container.AbsoluteLeft;
                    break;
                case Side.Right:
                    AbsoluteRight = Container.AbsoluteRight;
                    break;
            }
        }

        private void KeepProjectileInBounds()
        {
            if (ProjectileOutOfRight)
            {
                _projectile.AbsoluteRight = RightBound;
            }
            if (ProjectileOutOfLeft)
            {
                _projectile.AbsoluteLeft = LeftBound;
            }
        }

        private bool ProjectileOutOfRight => _projectile.AbsoluteRight > RightBound;

        private bool ProjectileOutOfLeft => _projectile.AbsoluteLeft < LeftBound;

        private float ProjectileLeftBound
        {
            get => Container.AbsoluteLeft + _projectile.AbsoluteLeft - AbsoluteLeft;
        }

        private float ProjectileRightBound
        {
            get => Container.AbsoluteRight - (AbsoluteRight - _projectile.AbsoluteRight);
        }
    }
}
