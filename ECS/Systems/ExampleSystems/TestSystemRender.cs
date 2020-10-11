using Hel.ECS.Components.Examples;
using Hel.ECS.Entities.Matcher;
using Hel.ECS.Entities.Matcher.Groups;
using Hel.ECS.Systems.Logic;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Systems.ExampleSystems
{
    public class TestCoupledSystemRender : IDrawSystem
    {
        
        /// <summary>
        /// Draws MUST run through the <see cref="SystemManager"/>. 
        /// </summary>
        public void Draw(float deltaTime, EntityMatcher matcher)
        {
            
            ((IEntityGroup<TransformExample, RenderExample>) matcher.GetEntityGroup(
                    new EntityGroupQuery()
                        .Containing<TransformExample>()
                        .Containing<RenderExample>()
                        .Build()))
                .ForEach((entityId, transform, render) =>
                {
                    
                    // Renderer implementation goes here

                    return default;
                });
          
        }
    }
}
