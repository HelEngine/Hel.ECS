using System;
using Hel.ECS.Components.Model;

namespace Hel.ECS.Components.Examples
{
    public struct TransformExample : IComponent, IEquatable<TransformExample>
    {
        /// <summary>
        /// The X position where your Texture2D will be placed when drawing
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y position where your Texture2D will be places when drawing
        /// </summary>
        public float Y { get; set; }
        public bool Active { get; set; }

        public TransformExample(uint x, uint y)
        {
            X = x;
            Y = y;
            Active = true;
        }

        public TransformExample(uint x, uint y, bool active)
        {
            X = x;
            Y = y;
            Active = active;
        }

        public bool Equals(TransformExample other)
        {
            return X.Equals(other.X) 
                   && Y.Equals(other.Y) 
                   && Active == other.Active;
        }
        
    }
}
