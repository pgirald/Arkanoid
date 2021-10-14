using Arkanoid.Application.App.Graphics;
using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using Arkanoid.Application.App.Graphics.Textures.Projectiles;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using Microsoft.Xna.Framework;

namespace Arkanoid.Application.App
{
    public class ArkanoidScenario : ScenarioTemplate
    {
        protected override void CreateGameItems()
        {
            Paddle = TexturesFactory.GetTextureClone<Paddle>();
            Projectile = TexturesFactory.GetTextureClone<Projectile>();
            Blocks = CreateSimpleBlocks();
        }

        protected override void SetGameItems()
        {
            Paddle.Align(Alignment.BottomCenter);
            Blocks.Align(Alignment.TopCenter);
            Projectile.PutOn(Paddle, Alignment.TopCenter);
            Paddle.Projectile = Projectile;
        }

        private BlockSet CreateSimpleBlocks()
        {
            BlockSet blocks = new BlockSet();
            for (int i = 5; i > 0; i--)
            {
                for (int j = 5; j > 0; j--)
                {
                    blocks.AddBlock<Block>(block=>block.Color = Color.LightCoral);
                }
                blocks.next();
            }
            return blocks;
        }
    }
}