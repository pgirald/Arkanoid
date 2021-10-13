using Arkanoid.Application.Utils;
using System;

namespace Arkanoid.Application.App.Components.Textures.Blocks
{
    public class Block : TextureComponent
    {
        public override string ParentPath => "Blocks/";

        public override string TexturePath => "Block";

        public float MarginLeft { get; set; } = 0;

        public float MarginTop { get; set; } = 0;

        public float MarginRight { get; set; } = 0;

        public float MarginBottom { get; set; } = 0;

        public virtual void Hit()
        {
            Destroyed?.Invoke(this, null);
        }
    }
}
