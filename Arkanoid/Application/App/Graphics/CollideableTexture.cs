using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Textures;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics
{
    public abstract class CollideableTexture : TextureComponent, ICollideable
    {
        public LinkedListNode<CollideableInfo> CMKey { get; set; }

        public abstract void OnCollision(Component collideable, CollisionInfo info);
    }
}
