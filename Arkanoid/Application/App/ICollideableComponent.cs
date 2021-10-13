using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.App.Components.Textures.Paddles;

namespace Arkanoid.Application.App
{
    public interface ICollideableComponent
    {
        public void OnPaddleHit(Paddle paddle, Side side);

        public void OnContainerHit(Side side);

        public void OnBlockHit(Block block, Side side);

        public void OnMovingComponentHit(CollideableComponent component, Side side);
    }
}
