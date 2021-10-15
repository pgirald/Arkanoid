using Arkanoid.Application.Utils.Components;
using Arkanoid.Application.Utils.GeneralExtensions;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Collisions
{
    public class CollisionManager
    {
        public CollisionManager()
        {
            _subscribers = new LinkedList<ICollideable>();
        }

        private LinkedList<ICollideable> _subscribers;

        public void Subscribe(ICollideable sub)
        {
            if (IsSub(sub))
            {
                throw new Exception("The specified component is already a subscriber");
            }
            sub.ManagerKey = _subscribers.AddLast(sub);
            sub.Key = Guid.NewGuid();
        }

        public void SubscribeVarious(params ICollideable[] subs)
        {
            foreach (ICollideable sub in subs)
            {
                Subscribe(sub);
            }
        }

        public void Unsuscribe(ICollideable sub)
        {
            if (!IsSub(sub))
            {
                throw new Exception("The specified component is not a subscriber");
            }
            Remove(sub);
        }

        private void Remove(ICollideable sub)
        {
            sub.Key = Guid.Empty;
            _subscribers.Remove(sub.ManagerKey);
            sub.ManagerKey = null;
            sub.SuscribedToComponents.Clear();
            sub.SpecialComponents.Clear();
        }

        public void LookForCollisions()
        {
            CollisionInfo info;
            _subscribers.ForAll(sub =>
            {
                sub.Value.SuscribedToComponents.ForAll(collideable =>
                {
                    info = collideable.Value.IntersectedWith((Component)sub.Value);
                    if (info != null)
                    {
                        Notify(sub.Value, collideable.Value, info);
                    }
                });
            });
        }

        public void AddSpecial(ICollideable collideable, ICollideable specialCollideable, ISpecialBehaviour behaviour)
        {
            if (!BothAreSubs(collideable, specialCollideable))
            {
                throw new Exception("One of the specified components is not a subscriber");
            }
            collideable.SpecialComponents.Add(specialCollideable.Key, behaviour);
        }

        public void SubscribeToComponent(ICollideable collideableSub, ICollideable collideable)
        {
            if (!BothAreSubs(collideableSub, collideable))
            {
                throw new Exception("One of the specified components is not a subscriber");
            }
            collideableSub.SuscribedToComponents.AddLast(collideable);
        }

        public void SubscribeToComponents(ICollideable collideableSub, params ICollideable[] collideables)
        {
            foreach (ICollideable collideable in collideables)
            {
                SubscribeToComponent(collideableSub, collideable);
            }
        }

        private bool IsSub(ICollideable collideable)
        {
            return collideable.ManagerKey != null && collideable.ManagerKey.List == _subscribers;
        }
        private bool BothAreSubs(ICollideable collideable1, ICollideable collideable2)
        {
            return IsSub(collideable1) || IsSub(collideable2);
        }

        private void Notify(ICollideable collideable, ICollideable collised, CollisionInfo info)
        {
            ISpecialBehaviour behaviour;
            if (collideable.SpecialComponents.TryGetValue(collised.Key, out behaviour))
            {
                behaviour.OnCollision((Component)collised, info);
            }
            else
            {
                collideable.OnCollision((Component)collised, info);
            }
        }

        public void Clear()
        {
            _subscribers.ForAll(node => Remove(node.Value));
        }
    }
}