using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.App.General
{
    public static class _2dTexturesFactory
    {
        private static Texture2D blockTexture;

        public static void Load(ContentManager content)
        {
            blockTexture = content.Load<Texture2D>("Blocks/Block");
        }

        public static Texture2D BlockTexture()
        {
            return blockTexture;
        }
    }
}
