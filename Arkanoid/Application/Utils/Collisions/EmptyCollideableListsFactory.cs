using System;
using System.Collections.Generic;

namespace Arkanoid.Application.Utils.Collisions
{
    public static class EmptyCollideableListsFactory
    {
        public static LinkedList<ICollideable> EmptyComponentsList;
        public static Dictionary<Guid, ISpecialBehaviour> EmptyCompsBehaviourDictionary;
        public static Dictionary<string, ISpecialBehaviour> EmptyTypesBehaviourDictionary;

        static EmptyCollideableListsFactory()
        {
            EmptyComponentsList = new LinkedList<ICollideable>();
            EmptyCompsBehaviourDictionary = new Dictionary<Guid, ISpecialBehaviour>();
            EmptyTypesBehaviourDictionary = new Dictionary<string, ISpecialBehaviour>();
        }
    }
}
