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
            _subscribers = new LinkedList<CollideableInfo>();
        }

        private LinkedList<CollideableInfo> _subscribers;

        private void CheckIfIsSub(ICollideable sub)
        {
            if (IsSub(sub))
            {
                throw new Exception("The specified component is already a subscriber");
            }
        }

        private void CheckIfIsNotSub(ICollideable sub)
        {
            if (!IsSub(sub))
            {
                throw new Exception("The specified component is not a sub");
            }
        }

        private void CheckIfBothAreSub(ICollideable collideable1, ICollideable collideable2)
        {
            if (!BothAreSubs(collideable1, collideable2))
            {
                throw new Exception("One of the specified components is not a subscriber");
            }
        }

        public void Subscribe(ICollideable sub)
        {
            CheckIfIsSub(sub);
            CollideableInfo info = new CollideableInfo(sub, Guid.NewGuid());
            sub.CMKey = _subscribers.AddLast(info);
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
            CheckIfIsNotSub(sub);
            Remove(sub);
        }

        private void Remove(ICollideable sub)
        {
            CollideableInfo subInfo = sub.CMKey.Value;
            subInfo.RemoveSubs();
            subInfo.SpecialBehaviours.Clear();
            _subscribers.Remove(sub.CMKey);
            sub.CMKey = null;
        }

        public void LookForCollisions()
        {
            CollisionInfo info;
            CollideableInfo collideableInfo;
            CollideableInfo collisedInfo;
            _subscribers.ForAll(sub =>
            {
                collideableInfo = sub.Value;
                collideableInfo.Subscribtions.ForAll(node =>
                {
                    collisedInfo = node.Value;
                    info = collisedInfo.Collideable.IntersectedWith((Component)collideableInfo.Collideable);
                    if (info != null)
                    {
                        Notify(collideableInfo, collisedInfo, info);
                    }
                });
            });
        }

        public void AddSpecial(ICollideable collideable, ICollideable specialCollideable, ISpecialBehaviour behaviour)
        {
            CheckIfBothAreSub(collideable, specialCollideable);
            collideable.CMKey.Value.SpecialBehaviours.Add(specialCollideable.CMKey.Value.Key, behaviour);
        }

        public void SubscribeToComponent(ICollideable collideableSub, ICollideable collideable)
        {
            CollideableInfo subInfo = collideableSub.CMKey.Value;
            subInfo.AddSubscribtion(collideable);
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
            return collideable.CMKey != null && collideable.CMKey.List == _subscribers;
        }
        private bool BothAreSubs(ICollideable collideable1, ICollideable collideable2)
        {
            return IsSub(collideable1) || IsSub(collideable2);
        }

        private void Notify(CollideableInfo collideableInfo, CollideableInfo collisedInfo, CollisionInfo info)
        {
            ISpecialBehaviour behaviour;
            if (collideableInfo.SpecialBehaviours.TryGetValue(collisedInfo.Key, out behaviour))
            {
                behaviour.OnCollision((Component)collisedInfo.Collideable, info);
            }
            else
            {
                collideableInfo.Collideable.OnCollision((Component)collisedInfo.Collideable, info);
            }
        }

        public void Replace(ICollideable oldColl, ICollideable newColl)
        {
            CheckIfIsNotSub(oldColl);
            oldColl.CMKey.Value.Collideable = newColl;
            newColl.CMKey = oldColl.CMKey;
            oldColl.CMKey = null;
        }

        public void Clear()
        {
            _subscribers.ForAll(node => Remove(node.Value.Collideable));
        }
    }
}