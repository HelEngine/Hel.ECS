using System;
using Hel.ECS.Components.Model;

namespace Hel.ECS.Components.Examples
{
    public struct MovementExample : IComponent, IEquatable<MovementExample>
    {
        public int Speed { get; set; }
        public bool Active { get; set; }

        public bool Equals(MovementExample other)
        {
            return Speed == other.Speed && Active == other.Active;
        }
    }

}
