using System;
using Hel.ECS.Components.Model;

namespace Hel.ECS.Entities.Matcher
{
    public class EntityGroupQuery
    {
        private int _query;

        public EntityGroupQuery Containing<T>() where T : IComponent
        {
            _query += typeof(T).GetHashCode();
            return this;
        }
        
        public EntityGroupQuery Containing(Type type)
        {
            _query += type.GetHashCode();
            return this;
        }

        public EntityGroupQuery Containing(Type[] types)
        {
            foreach (var type in types)
            {
                Containing(type);
            }

            return this;
        }

        public int Build()
        {
            return _query;
        }
    }
}