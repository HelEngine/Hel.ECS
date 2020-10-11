using Hel.ECS.Components.Examples;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Systems.ExampleSystems
{
    public class TestSystemMovementIncreaseDecoupled : ISystem<TransformExample, MovementExample>
    {
        public (TransformExample?, MovementExample?) RunSystem(float deltaTime, int entityId, TransformExample transformExample, MovementExample movementExample)
        {
            movementExample.Speed += 5;
            return (transformExample, movementExample);
        }
    }
}