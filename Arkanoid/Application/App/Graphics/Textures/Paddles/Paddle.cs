using Arkanoid.Application.App.Graphics.Enums;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arkanoid.Application.App.Graphics.Textures.Paddles
{
    public class Paddle : AnimatedTexture
    {
        public static string PaddleParentPath => "Paddles/";

        public override string ParentPath => PaddleParentPath;

        public override string TexturePath => "Paddle";

        public Paddle()
        {
            Speed = 500f;
        }

        private float LeftBound;
        private float RightBound;
        private Projectile _projectile;

        public override void Move(GameInfo info)
        {
            float computedSpeed = info.ComputedSpeed;
            KeyboardState state = info.KeyboardState;
            float direction = 0;
            if (state.IsKeyDown(AppControls.Right))
            {
                direction = 1;
            }
            if (state.IsKeyDown(AppControls.Left))
            {
                direction = -1;
            }
            X += computedSpeed * direction;
            if (_projectile.Resting)
            {
                _projectile.X += computedSpeed * direction;
                direction = direction == 0 ? 1 : direction;
                _projectile.FixTo((ProjectileDirection)direction);
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

        public void LeaveProjectile()
        {
            _projectile.RestStart -= OnProjetileRestStart;
            _projectile = null;
        }

        public override void OnCollision(Component collideable, CollisionInfo info)
        {
            Side side = info.Side;
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
