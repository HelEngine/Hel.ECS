using Hel.ECS.Components.Model;
using Hel.ECS.Entities.Matcher;

namespace Hel.ECS.Systems.System
{
    public interface ISystem<TComponent1>
    where TComponent1 : struct, IComponent
    {
        TComponent1? RunSystem(float deltaTime, int entityId, TComponent1 component);
    }
    
    public interface ISystem<TComponent1, TComponent2>
    where TComponent1 : struct, IComponent
    where TComponent2 : struct, IComponent
    {
        (TComponent1?, TComponent2?) RunSystem(float deltaTime, int entityId, TComponent1 component, TComponent2 component2);
    }
    
    public interface ISystem<TComponent1, TComponent2, TComponent3>
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
    {
        (TComponent1?, TComponent2?, TComponent3?) RunSystem(
            float deltaTime, int entityId, TComponent1 component, TComponent2 component2, TComponent3 component3);
    }
    
    /*
    public interface ISystem<TComponent1, TComponent2, TComponent3, TComponent4>
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
        where TComponent3 : struct, IComponent
        where TComponent4 : struct, IComponent
    {
        (TComponent1?, TComponent2?, TComponent3?, TComponent4?) RunSystem(
            float deltaTime, int entityId, TComponent1 component, TComponent2 component2, TComponent3 component3, TComponent4 component4);
    }*/

    public interface ISystemBase { }
    
    public interface IDrawSystem : ISystemBase
    {
        void Draw(float deltaTime, EntityMatcher matcher);
    }
    
    public interface IUpdateSystem : ISystemBase
    {
        void Update(float deltaTime, EntityMatcher matcher);
    }
}