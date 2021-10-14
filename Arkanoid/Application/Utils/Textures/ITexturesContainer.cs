using System;
using System.Collections.Generic;
using System.Text;

namespace Arkanoid.Application.Utils.Textures
{
    public interface ITexturesContainer
    {
        public IEnumerable<TextureComponent> Textures { get; }
    }
}
