using Arkanoid.Application.App.Graphics.Textures.Blocks;
using Arkanoid.Application.App.Graphics.Textures.Paddles;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Collisions
{
    public abstract class CollideableComponent : Component
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
