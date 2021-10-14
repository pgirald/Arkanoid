namespace Arkanoid.Application.Utils.Collisions
{
    public interface IAnimatedComponent
    {
        public float Speed { get; set; }

        public abstract void Move(float ellapsedTipe);
    }
}
