using Arkanoid.Application.Utils.Components;

namespace Arkanoid.Application.Utils.Collisions
{
    public interface ISpecialBehaviour
    {
        public void OnCollision(Component component, CollisionInfo info);
    }
}
