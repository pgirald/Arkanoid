using Arkanoid.Application.Utils.Textures;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Arkanoid.Application.App.Graphics
{
    public static class TexturesFactory
    {
        private static Dictionary<string, TextureComponent> Textures;
        private static bool FirstTime = true;

        private static Type[] TextureTypes
        {
            get
            {
                string texturesNamespace = nameof(Arkanoid) + "." +
                    nameof(Application) + "." +
                    nameof(App) + "." +
                    nameof(Graphics) + "." +
                    nameof(Graphics.Textures) + ".";
                return Assembly.GetExecutingAssembly().GetTypes().
                Where(t => t.FullName.StartsWith(texturesNamespace)).
                ToArray();
            }
        }

        public static void Load(ContentManager content)
        {
            if (!FirstTime)
            {
                throw new Exception("The components where alredy loaded");
            }

            FirstTime = false;

            Textures = new Dictionary<string, TextureComponent>(StringComparer.Create(CultureInfo.InvariantCulture, true));

            foreach (Type textureType in TextureTypes)
            {
                TextureComponent textureComponent = (TextureComponent)Activator.CreateInstance(textureType);
                textureComponent.Texture = content.Load<Texture2D>(textureComponent.FullPath);
                Textures.Add(textureType.FullName, textureComponent);
            }
        }

        public static T GetTexture<T>() where T : TextureComponent
        {
            return (T)Textures[typeof(T).FullName];
        }

        public static T GetTextureClone<T>() where T : TextureComponent, new()
        {
            return GetTexture<T>().Clone<T>();
        }
    }
}
