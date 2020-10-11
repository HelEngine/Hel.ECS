using System;

namespace Hel.ECS.Components.Examples
{
  
    public struct RenderExample : IRenderComponentExample, IEquatable<RenderExample>
    {
        /// <summary>
        /// Texture is the Texture2D your entity should draw to the screen 
        /// </summary>
        //public Texture2D Texture { get; set; }
        
        public ushort ZIndex { get; set; }
        public bool Active { get; set; }
        

        public bool Equals(RenderExample other)
        {
            return ZIndex == other.ZIndex 
                   && Active == other.Active;
        }
    }
}
