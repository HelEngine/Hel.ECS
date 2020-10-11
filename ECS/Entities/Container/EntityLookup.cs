using System;
using System.Collections.Generic;
using Hel.Toolkit.DataStructure.Arrays;

namespace Hel.ECS.Entities.Container
{
    /// <summary>
    /// EntityLookup is the source of truth regarding currently available and allocated entities.
    /// Entities are stored as Id => String using a <see cref="ManagedArray{TDataType}"/>
    /// </summary>
    public class EntityLookup
    {
        private readonly ManagedArray<string> _entityTypeLookup;

        public EntityLookup()
        {
            _entityTypeLookup = new ManagedArray<string>(256);
        }

        public int EntityCount => _entityTypeLookup.DataCount;
        
        /// <summary>
        /// Retrieve all entity ID's that match/contain the provided name
        /// </summary>
        /// <param name="name">The name of the entity</param>
        /// <returns>List of entity ID's that match the name</returns>
        public List<int> GetByName(string name)
        {
            var entities = new List<int>();
            
            for (int i = 0; i < _entityTypeLookup.Size; i++)
            {
                if (_entityTypeLookup[i].Contains(name))
                {
                    entities.Add(i);
                }
            }
            
            return entities;
        }
        
        /// <summary>
        /// Retrieve the first entity that matches the provided name
        /// </summary>
        /// <param name="name">The name of the entity</param>
        /// <returns>The first entity ID to match the name</returns>
        public int GetFirstByName(string name)
        {
            for (int i = 0; i < _entityTypeLookup.Size; i++)
            {
                if (_entityTypeLookup[i].Contains(name))
                {
                    return i;
                }
            }
            
            return -1;
        }

        /// <summary>
        /// Get all entities allocated
        /// </summary>
        /// <returns>List of entity ID's</returns>
        public IEnumerable<int> GetEntities()
        {
            return _entityTypeLookup.GetNonNullIndexes();
        }
        
        /// <summary>
        /// Override an entitys name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void OverrideEntity(int id, string name)
        {
            _entityTypeLookup[id] = name;
        }

        /// <summary>
        /// Remove all entities that match a name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void RemoveAllWithName(int id, string name)
        {
            var entities = GetByName(name);
            for (int i = 0; i < entities.Count; i++)
            {
                _entityTypeLookup.Remove(entities[i]);
            }
        }

        /// <summary>
        /// Remove entity by ID
        /// </summary>
        /// <param name="id"></param>
        public void RemoveEntity(int id) => _entityTypeLookup.Remove(id);

        /// <summary>
        /// Add entity by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Add(string name) => _entityTypeLookup.Add(name);

        /// <summary>
        /// Add entity and set entity name to new guid
        /// </summary>
        /// <returns></returns>
        public int Add() => Add(Guid.NewGuid().ToString());

        public void Clear()
        {
            _entityTypeLookup.Clear();
        }

    }
}
