using Arkanoid.Application.Utils.Collisions;
using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.Textures;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.App.Graphics
{
    public abstract class CollideableTexture : TextureComponent, ICollideable
    {
        public CollideableTexture()
        {
            SuscribedToComponents = new List<ICollideable>();
            SpecialComponents = new Dictionary<Guid, ISpecialBehaviour>();
        }

        public LinkedListNode<ICollideable> ManagerKey { get; set; }

        public Guid Key { get; set; }

        public List<ICollideable> SuscribedToComponents { get; private set; }

        public Dictionary<Guid, ISpecialBehaviour> SpecialComponents { get; private set; }

        public abstract void OnCollision(Component collideable, CollisionInfo info);
    }
}
