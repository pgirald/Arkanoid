using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.App.Components.Textures.Paddles;
using Arkanoid.Application.Utils;

namespace Arkanoid.Application.App
{
    public abstract class CollideableComponent : AnimatedComponent, ICollideableComponent
    {
        public virtual void OnBlockHit(Block block, Side side) { }

        public virtual void OnContainerHit(Side side) { }

        public virtual void OnPaddleHit(Paddle paddle, Side side) { }

        public virtual void OnMovingComponentHit(CollideableComponent component, Side side) { }
    }
}
