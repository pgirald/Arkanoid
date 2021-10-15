using Arkanoid.Application.Utils.Textures;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Application.Utils.GeneralExtensions
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

        public static void Draw(this SpriteBatch spriteBatch, IDrawable drawable, SpriteEffects effects = SpriteEffects.None)
        {
            foreach (TextureComponent texture in drawable.Textures)
            {
                spriteBatch.Draw(texture, effects);
            }
        }
    }
}
