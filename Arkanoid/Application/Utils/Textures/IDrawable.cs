using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Textures
{
    public interface IDrawable
    {
        public IEnumerable<TextureComponent> Textures { get; }
    }
}
