using Hel.ECS.Components.Model;

namespace Hel.ECS.Components.Examples
{
    public interface IRenderComponentExample : IComponent
    {
        public ushort ZIndex { get; set; }
    }
}