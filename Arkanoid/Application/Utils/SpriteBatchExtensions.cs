using Arkanoid.Application.App;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Application.Utils
{
    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, TextureComponent component, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(component,
                component.AbsolutePosition,
                component.TrimRectangle,
                component.Color,
                component.Rotation,
                component.Origin,
                component.Scale,
                effects,
                component.LayerDepth);
        }

        public static void Draw(this SpriteBatch spriteBatch, BlocksSet container)
        {
            foreach (BlocksRow row in container.Childs)
                foreach (Block block in row.Childs)
                {
                    Draw(spriteBatch, block);
                }
        }
    }
}
