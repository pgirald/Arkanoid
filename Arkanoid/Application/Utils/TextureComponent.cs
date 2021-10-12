﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid.Application.Utils
{
    public class TextureComponent : Component
    {
        public TextureComponent()
        {
            Scale = Vector2.One;
        }

        private Texture2D _texture;

        public virtual string ParentPath { get => ""; }

        public virtual string TexturePath { get => throw new NotImplementedException(); }

        public Texture2D Texture
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

        public Rectangle? TrimRectangle { get; set; } = null;

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
    }
}