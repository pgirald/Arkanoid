using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Game
{
    public interface IAnimated : IDrawable
    {
        public LinkedListNode<Component> AnimatedKey{ get; set; }

        public float Speed { get; set; }

        public abstract void Move(GameInfo info);
    }
}
