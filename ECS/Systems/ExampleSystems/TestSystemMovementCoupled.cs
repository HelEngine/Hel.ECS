using System.Numerics;
using Hel.ECS.Components.Examples;
using Hel.ECS.Entities.Matcher;
using Hel.ECS.Entities.Matcher.Groups;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Systems.ExampleSystems
{
    public class TestCoupledSystemMovementCoupled : IUpdateSystem
    {
        
        /// <summary>
        /// Called by SystemManager
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <param name="matcher"></param>
        public void Update(float deltaTime, EntityMatcher matcher)
        {

            ((IEntityGroup<MovementExample, TransformExample>) matcher.GetEntityGroup(
                    new EntityGroupQuery()
                        .Containing<MovementExample>()
                        .Containing<TransformExample>()
                        .Build()))
                .ForEach((entityId, movement, transform) =>
                {
                    // Get keyboard input here. 
                    var moveDirection = Vector2.One;

                    if (moveDirection != Vector2.Zero)
                    {
                        transform.X += (moveDirection.X * movement.Speed) * deltaTime;
                        transform.Y += (moveDirection.Y * movement.Speed) * deltaTime;
                        return (default, transform);
                    }

                    // Don't return anything if nothing is updated. This helps performance
                    return default;
                });
        }
        
    }
}