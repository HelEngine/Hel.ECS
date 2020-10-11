using Hel.ECS.Components.Examples;
using Hel.ECS.Entities.Matcher;
using Hel.ECS.Entities.Matcher.Groups;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Systems.ExampleSystems
{
    public class TestCoupledSystemMovementIncreaseCoupled : IUpdateSystem
    {
        
        /// <summary>
        /// The system manager will run this every update. This runs AFTER the <see cref="EntityGroup{TComponent1}" events/>
        /// </summary>
        public void Update(float deltaTime, EntityMatcher matcher)
        {
            // Order of generics for casting matters
            ((IEntityGroup<TransformExample, MovementExample>) matcher.GetEntityGroup(
                    new EntityGroupQuery()
                        // Containing order does not matter
                        .Containing<TransformExample>()
                        .Containing<MovementExample>()
                        .Build()))
                .ForEach((entityId, transform, movement) =>
                {
                    movement.Speed += 5;
                    return (transform, movement);
                });
        }
    }
}