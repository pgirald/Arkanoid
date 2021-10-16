using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Application.App.Graphics.Textures.Blocks
{
    public class ToughBlock : Block
    {
        private bool _hitedOnce = false;

        public override string TexturePath => "Tough_Block";

        public override float Width => base.Width / 2;

        public override Texture2D Texture
        {
            set
            {
                base.Texture = value;
                TrimRectangle = new Rectangle(0, 0, (int)(base.Width / 2), (int)Height);
            }
        }

        public override void Hit()
        {
            if (!_hitedOnce)
            {
                TrimRectangle = new Rectangle((int)(base.Width / 2), 0, (int)(base.Width / 2), (int)Height);
                _hitedOnce = true;
                return;
            }
            TryItemGeneration();
            Destroyed?.Invoke(this, null);
        }
    }
}
