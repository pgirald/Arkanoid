using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Arkanoid.Application.Utils.Game.DynamicDrawing;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics.Textures.Paddles
{
    public class GunPaddle : Paddle, IDrawer
    {
        private bool CanShoot = true;

        public GunPaddle()
        {
            Color = Color.OrangeRed;
        }

        public LinkedListNode<IDrawer> ScenarioKey { get; set; }

        public EventHandler<DrawEventArgs> DrawComponent { get; set; }

        public override void Move(GameInfo info)
        {
            base.Move(info);
            Bullet bullet = TexturesFactory.GetTextureClone<Bullet>();
            bullet.PutOn(this, Alignment.TopCenter);
            bullet.Destroyed += OnBulletDestroyed;
            if (info.KeyboardState.IsKeyDown(AppControls.Up) && CanShoot && !Projectile.Resting)
            {
                DrawComponent(this, new DrawEventArgs { Draw = bullet });
                CanShoot = false;
            }
        }

        public void OnBulletDestroyed(object sender, EventArgs args)
        {
            CanShoot = true;
        }
    }
}
