using Arkanoid.Application.App.Components;
using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.App.Components.Textures.Paddles;
using Arkanoid.Application.App.Components.Textures.Projectiles;
using Arkanoid.Application.App.General;
using Arkanoid.Application.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App
{
    public class ClassicScenario : ScenarioTemplate
    {
        public ClassicScenario(int width, int height)
        {
            Width = width;
            Height = height;
            Paddle.Align(Alignment.BottomCenter);
            Blocks.Align(Alignment.TopRight);
            Projectile.PutOn(Paddle, Alignment.TopCenter);
        }

        protected override void SetGameItems()
        {
            Paddle = TexturesFactory.GetTexture<Paddle>();
            Projectile = TexturesFactory.GetTexture<Projectile>();
            Blocks = CreateSimpleBlocks();
        }

        private BlockSet CreateSimpleBlocks()
        {
            BlockSet blocks = new BlockSet();
            for(int i = 1; i > 0; i--)
            {
                for (int j =1; j > 0; j--)
                {
                    blocks.AddBlock<Block>(block=>block.Color = Color.Red);
                }
                blocks.next();
            }
            return blocks;
        }
    }
}
