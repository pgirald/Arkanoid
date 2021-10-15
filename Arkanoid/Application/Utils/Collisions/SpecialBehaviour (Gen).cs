using Arkanoid.Application.Utils.Components;

namespace Arkanoid.Application.Utils.Collisions
{
    public abstract class SpecialBehaviour<T> : ISpecialBehaviour where T : Component
    {
        public void OnCollision(Component component, CollisionInfo info)
        {
            OnCollision((T)component, info);
        }

        public abstract void OnCollision(T component, CollisionInfo info);
    }
}
