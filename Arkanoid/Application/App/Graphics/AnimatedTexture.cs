using Arkanoid.Application.Utils.Game;

namespace Arkanoid.Application.App.Graphics
{
    public abstract class AnimatedTexture : CollideableTexture, IAnimated
    {
        public float Speed { get; set; }

        public abstract void Move(float computedSpeed);
    }
}
