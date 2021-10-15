using Arkanoid.Application.Utils.Textures;

namespace Arkanoid.Application.Utils.Game
{
    public interface IAnimated : IDrawable
    {
        public float Speed { get; set; }

        public abstract void Move(float computedSpeed);
    }
}
