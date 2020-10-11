using System;
using System.Collections.Generic;
using System.Linq;
using Hel.ECS.Components.Container;
using Hel.ECS.Components.Model;
using Hel.ECS.Entities.Container;
using Hel.ECS.Entities.Models;

namespace Hel.ECS.Entities.Logic
{
    public interface IEntityManager
    {
        /// <summary>
        /// The world that this entity manager belongs to
        /// </summary>
        World.World World { get; }
        /// <summary>
        /// Add an entity to the world. An entity is any struct with the IEntity interface implemented.
        /// </summary>
        /// <param name="entity">Struct with the IEntity interface</param>
        /// <returns>The ID of the generated entity</returns>
        int AddEntity(IEntity entity);
        /// <summary>
        /// Fetches all available entity ID's
        /// </summary>
        /// <returns>List containing all entities</returns>
        IEnumerable<int> GetEntityIds();

    }

    /// <summary>
    /// EntityManager stores entity data and provides a safe way to modify and retrieve entities.
    /// </summary>
    public class EntityManager : IEntityManager
    {
        private readonly EntityLookup _entityLookup = new EntityLookup();
        private Dictionary<Type, ComponentContainer> _components;

        public World.World World { get; private set; }

        /// <summary>
        /// How many entities exist
        /// </summary>
        public int EntityCount => _entityLookup.EntityCount;

        public EntityManager(World.World world)
        {
            World = world;
            _components = new Dictionary<Type, ComponentContainer>();

            GenerateComponentContainers();
        }

        public Dictionary<Type, ComponentContainer> GetComponentDictionary() => _components;

        private void GenerateComponentContainers()
        {
            var componentStructs = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IComponent).IsAssignableFrom(x) && !x.IsInterface && x.IsValueType);

            foreach (var type in componentStructs)
            {
                _components.Add(type, new ComponentContainer(type));
            }
        }
        
        public int AddEntity(IEntity entity)
        {
            return AddEntity(entity.Components);
        }

        public int AddEntity(HashSet<IComponent> components)
        {
            return AddEntity(components, null);
        }
        
        public int AddEntity(HashSet<IComponent> components, string name)
        {
            lock(_entityLookup)
            lock (_components)
            {
                var id = name == null ? _entityLookup.Add() : _entityLookup.Add(name);
                
                foreach (var component in components)
                {
                    var compType = component.GetType();
                
                    if (_components.TryGetValue(compType, out var tryGetComp))
                    {
                        tryGetComp.UpdateComponent(id, component);
                        continue;
                    }
                    // Dict does not contain the component type, add it. 
                    var compByEnt = new ComponentContainer(compType);
                    compByEnt.UpdateComponent(id, component);
                    _components.Add(compType, compByEnt);
                }

                return id;
            }
        }

        public int GetEntityId(string name)
        {
            lock(_entityLookup)
            lock (_components)
            {
                return _entityLookup.GetFirstByName(name);
            }
        }
        
        public IEnumerable<int> GetEntityIds()
        {
            lock(_entityLookup)
            lock (_components)
            {
                return _entityLookup.GetEntities();
            }
        }

        public void RemoveEntity(int id)
        {
            lock(_entityLookup)
            lock (_components)
            {
                _entityLookup.RemoveEntity(id);
                foreach (var componentContainer in _components)
                {
                    componentContainer.Value.Remove(id);
                }
            }
        }

        public void ClearEntities()
        {
            lock(_entityLookup)
            lock (_components)
            {
                _entityLookup.Clear();
                _components.Clear();
            }
        }

        public void UpdateEntitiesInStaging()
        {
            foreach (var componentContainer in _components)
            {
                componentContainer.Value.ApplyUpdates();
            }
        }
    }
}
 
