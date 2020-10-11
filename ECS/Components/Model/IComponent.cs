namespace Hel.ECS.Components.Model
{
    /// <summary>
    /// Marks a struct as a component that can be linked to an entity.
    /// This is the base interface all components MUST implement.
    ///
    /// A component is a struct that contains single purpose values that is passed to one or multiple systems in-order
    /// to run the logic associated.
    ///
    /// For example, a 2Dtransform component would contain X and Y location data, X and Y rotation and X and Y scale. 
    /// </summary>
    public interface 
        
        IComponent
    {
        /// <summary>
        /// Allows if the component should be used or not.
        /// </summary>
        bool Active { get; set; }
    }

}
