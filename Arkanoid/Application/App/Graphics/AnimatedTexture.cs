using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Game;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics
{
    public abstract class AnimatedTexture : CollideableTexture, IAnimated
    {
        public float Speed { get; set; }

        public LinkedListNode<Component> AnimatedKey { get; set; }

        public abstract void Move(GameInfo info);
    }
}
