using System.Collections.Generic;
using Hel.ECS.Components.Model;

namespace Hel.ECS.Entities.Models
{
    /// <summary>
    /// IEntity is the base interface that components should use.
    /// These entities are not stored as objects but rather deconstructed into an ID and a list of _components.
    ///
    /// You can add entities directly without using an IEntity, if required.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The name to give the entity. This allows an easy way to retrieve the entity later on. 
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// List of Icomponents that will be assigned in the EntityManager
        /// </summary>        
        HashSet<IComponent> Components { get; }

    }

    public struct Entity : IEntity
    {
        public HashSet<IComponent> Components { get; }

        public string Name { get; set; }
        
        public Entity(string name, params IComponent[] componentList)
        {
            Components = new HashSet<IComponent>();
            Name = name;
            foreach (var component in componentList)
            {
                Components.Add(component);   
            }
        }
    }

}
