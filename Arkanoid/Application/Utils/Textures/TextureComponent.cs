using Arkanoid.Application.Utils.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Textures
{
    public class TextureComponent : Component, IDrawable
    {
        public TextureComponent()
        {
            Scale = Vector2.One;
        }

        private Texture2D _texture;

        public virtual string ParentPath { get => ""; }

        public virtual string TexturePath { get => throw new NotImplementedException(); }

        public string FullPath => ParentPath + TexturePath;

        public virtual Texture2D Texture
        {
            set
            {
                {
                    if (_texture != null)
                    {
                        throw new Exception("Texture was already established");
                    }
                    _texture = value;
                }
            }
        }

        public Vector2 Scale { get; set; }

        public Rectangle? TrimRectangle { get; set; }

        public Rectangle Destination { get; set; }

        public override float Width => _texture.Width;

        public override float Height => _texture.Height;

        public Color Color { get; set; } = Color.White;

        public float LayerDepth { get; set; } = 0f;

        public float Rotation { get; set; } = 0f;

        public static implicit operator Texture2D(TextureComponent component)
        {
            return component._texture;
        }


        public void ShareTexture(TextureComponent textureComponent)
        {
            textureComponent.Texture = _texture;
        }

        public virtual T Clone<T>() where T : TextureComponent, new()
        {
            T textureComponent = new T();
            textureComponent.Texture = _texture;
            return textureComponent;
        }

        public TextureComponent Clone()
        {
            TextureComponent texture = (TextureComponent)Activator.CreateInstance(GetType());
            texture.Texture = _texture;
            return texture;
        }

        public IEnumerable<TextureComponent> Textures
        {
            get
            {
                yield return this;
            }
        }
    }
}
