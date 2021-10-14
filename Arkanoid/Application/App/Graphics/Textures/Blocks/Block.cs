using Arkanoid.Application.Utils.Collisions;

namespace Arkanoid.Application.App.Graphics.Textures.Blocks
{
    public class Block : CollideableComponent, ICollideableComponent
    {
        public override string ParentPath => "Blocks/";

        public override string TexturePath => "Block";

        public float MarginLeft { get; set; } = 0;

        public float MarginTop { get; set; } = 0;

        public float MarginRight { get; set; } = 0;

        public float MarginBottom { get; set; } = 0;

        public override void OnMovingComponentHit(CollideableComponent component, Side side)
        {
            Destroyed?.Invoke(this, null);
        }

        protected override void Proceed(float trueSpeed)
        {
            throw new System.NotImplementedException();
        }
    }
}
