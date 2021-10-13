using Arkanoid.Application.App.Components.Textures.Blocks;
using Arkanoid.Application.App.Components.Textures.Paddles;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App
{
    public abstract class CollideableComponent : AnimatedComponent, ICollideableComponent
    {
        public CollideableComponent()
        {
            CollisionKeys = new LinkedListNode<CollideableComponent>[Enum.GetValues(typeof(Subscription)).Length];
        }

        public LinkedListNode<CollideableComponent>[] CollisionKeys { get; set; }

        public virtual void OnBlockHit(Block block, Side side) { }

        public virtual void OnContainerHit(Side side) { }

        public virtual void OnPaddleHit(Paddle paddle, Side side) { }

        public virtual void OnMovingComponentHit(CollideableComponent component, Side side) { }
    }
}
