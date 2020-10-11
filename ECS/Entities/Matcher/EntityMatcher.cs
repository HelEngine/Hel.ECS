using System;
using System.Collections;
using System.Collections.Generic;
using Hel.ECS.Components.Model;
using Hel.ECS.Entities.Matcher.Groups;
using Hel.ECS.Exceptions;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Entities.Matcher
{
    public class EntityMatcher : IEnumerable<IEntityGroup>
    {

        private readonly Dictionary<int, IEntityGroup> _entityGroups = new Dictionary<int, IEntityGroup>();

        private World.World World { get; }

        public EntityMatcher(World.World world)
        {
            World = world;
        }
        
        public IEntityGroup GetEntityGroup(int query)
        {
            if(_entityGroups.TryGetValue(query, out var getEntityMatcher))
            {
                return getEntityMatcher;
            }
            
            throw new InvalidEntityGroupException("System not registered");
        }

        private int GroupValidator(params Type[] types)
        {
            var query = new EntityGroupQuery().Containing(types).Build();
            if (_entityGroups.ContainsKey(query))
            {
                throw new InvalidEntityGroupException($"EntityGroup for {types} already exists!");
            }

            return query;
        }
        
        // ONE GENERIC CONSTRAINT
        
        public EntityGroup<T> Register<T>() where T: struct, IComponent
        {
            var query = GroupValidator(typeof(T));
            
            var entityGroup =
                new EntityGroup<T>(World.EntityManager.GetComponentDictionary());
            
            _entityGroups.Add(query, entityGroup);

            return entityGroup;
        }
        
        public EntityGroup<T> Register<T>(params ISystem<T>[] systems) where T: struct, IComponent
        {
            var entityGroup = Register<T>();
            entityGroup.AddSystems(systems);

            return entityGroup;

        }
        
        // TWO GENERIC CONSTRAINT
        
        public EntityGroup<T, T2> Register<T, T2>() 
            where T : struct, IComponent
            where T2: struct, IComponent
        {
            var query = GroupValidator(typeof(T), typeof(T2));
            
            var entityGroup =
                new EntityGroup<T, T2>(World.EntityManager.GetComponentDictionary());
            
            _entityGroups.Add(query, entityGroup);

            return entityGroup;
        }
        
        public EntityGroup<T, T2> Register<T, T2>(params ISystem<T, T2>[] systems) 
            where T : struct, IComponent
            where T2: struct, IComponent
        {

            var entityGroup = Register<T, T2>();
            entityGroup.AddSystems(systems);

            return entityGroup;
        }
        
        // THREE GENERIC CONSTRAINT
        
        public EntityGroup<T, T2, T3> Register<T, T2, T3>()
            where T : struct, IComponent
            where T2: struct, IComponent
            where T3 : struct, IComponent
        {
            var query = GroupValidator(typeof(T), typeof(T2), typeof(T3));
            
            var entityGroup =
                new EntityGroup<T, T2, T3>(World.EntityManager.GetComponentDictionary());
            
            _entityGroups.Add(query, entityGroup);

            return entityGroup;
        }
        
        public EntityGroup<T, T2, T3> Register<T, T2, T3>(params ISystem<T, T2, T3>[] systems)
            where T : struct, IComponent
            where T2: struct, IComponent
            where T3 : struct, IComponent
        {
            var entityMatcher = Register<T, T2, T3>();
            entityMatcher.AddSystems(systems);

            return entityMatcher;
        }

        // ENUMERATORS 
        
        public IEnumerator<IEntityGroup> GetEnumerator()
        {
            foreach (var entityMatcher in _entityGroups)
            {
                yield return entityMatcher.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}