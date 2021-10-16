using Arkanoid.Application.Utils.GeneralExtensions;
using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Collisions
{
    public class CollideableInfo
    {

        public CollideableInfo(ICollideable collideable, Guid key)
        {
            Subscribers = new LinkedList<KeyValuePair<LinkedListNode<CollideableInfo>, CollideableInfo>>();
            Subscribtions = new LinkedList<CollideableInfo>();
            SpecialBehaviours = new Dictionary<Guid, ISpecialBehaviour>();
            Collideable = collideable;
            Key = key;
        }

        public Guid Key { get; }

        public ICollideable Collideable { get; set; }

        public LinkedList<KeyValuePair<LinkedListNode<CollideableInfo>, CollideableInfo>> Subscribers { get; }

        public LinkedList<CollideableInfo> Subscribtions { get; }

        public Dictionary<Guid, ISpecialBehaviour> SpecialBehaviours { get; }

        public void AddSubscriber(ICollideable sub)
        {
            CollideableInfo subInfo = sub.CMKey.Value;
            AddSubscriber(subInfo);
        }

        private void AddSubscriber(CollideableInfo sub)
        {
            KeyValuePair<LinkedListNode<CollideableInfo>, CollideableInfo> pair = new
                KeyValuePair<LinkedListNode<CollideableInfo>, CollideableInfo>(
                sub.Subscribtions.AddLast(this),
                sub);
            Subscribers.AddLast(pair);
        }

        public void RemoveSubs()
        {
            Subscribers.ForAll(sub =>
            {
                KeyValuePair<LinkedListNode<CollideableInfo>, CollideableInfo> pair =
                    sub.Value;
                CollideableInfo subInfo = pair.Value;
                if (subInfo.Subscribtions.Count > 0)
                {
                    subInfo.Subscribtions.Remove(pair.Key);
                }
            });
            Subscribers.Clear();
            Subscribtions.Clear();
        }

        public void AddSubscribtion(ICollideable collideable)
        {
            collideable.CMKey.Value.AddSubscriber(this);
        }

        public void AddSpecial(ICollideable collideable, ISpecialBehaviour behaviour)
        {
            CollideableInfo collInfo = collideable.CMKey.Value;
            SpecialBehaviours.Add(collInfo.Key, behaviour);
        }
    }
}
