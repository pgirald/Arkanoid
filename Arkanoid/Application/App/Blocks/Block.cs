using Arkanoid.Application.Utils;

namespace Arkanoid.Application.App
{
    public class Block : AnimatedTexture
    {
        public override string ParentPath => "Blocks/";

        public override string TexturePath => "Block";

        public float MarginLeft { get; set; } = 0;

        public float MarginTop { get; set; } = 0;
    }
}
