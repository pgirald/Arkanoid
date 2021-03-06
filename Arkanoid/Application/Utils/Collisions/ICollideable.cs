using Arkanoid.Application.Utils.Components;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Collisions
{
    public interface ICollideable
    {
        public LinkedListNode<CollideableInfo> CMKey { get; set; }

        public void OnCollision(Component collideable, CollisionInfo info)
        {
            throw new NotImplementedException("The class does'nt implement this method atohugh it is subscribed to some compoenents");
        }

        public CollisionInfo IntersectedWith(Component collideable)
        {
            if (CollisionOps.AreIntersected((Component)this, collideable))
            {
                return CollisionOps.GetCollisionInfo((Component)this, collideable);
            }
            return null;
        }
    }
}
