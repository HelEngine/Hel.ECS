using System.Numerics;
using Hel.ECS.Components.Examples;
using Hel.ECS.Entities.Matcher.Groups;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Systems.ExampleSystems
{
    /// <summary>
    /// System generic order matters
    /// </summary>
    public class TestSystemMovementDecoupled : ISystem<MovementExample, TransformExample>
    {
        /// <summary>
        /// Is run by <see cref="EntityGroup{TComponent1,TComponent2}"/> every update
        /// </summary>
        /// <returns>Returns modified data to the <see cref="EntityGroup{TComponent1, TComponent2}"/> and updates the component containers</returns>
        public (MovementExample?, TransformExample?) RunSystem(float deltaTime, int entityId, MovementExample movementExample, TransformExample transformExample)
        {
            // Get keyboard input here
            var moveDirection = Vector2.One;

            if (moveDirection != Vector2.Zero)
            {
                transformExample.X += (moveDirection.X * movementExample.Speed) * deltaTime;
                transformExample.Y += (moveDirection.Y * movementExample.Speed) * deltaTime;
                return (default, transformExample);
            }

            // For performance reasons, only return data that has been modified and needs to be updated
            return default;

        }
    }
}