using System;
using System.Collections.Generic;
using Hel.ECS.Systems.System;

namespace Hel.ECS.Systems.Logic
{

    public class SystemManager
    {
        private readonly List<IUpdateSystem> _updateSystems = new List<IUpdateSystem>();
        private readonly List<IDrawSystem> _drawSystems = new List<IDrawSystem>();
        public World.World World { get; }
        
        public SystemManager(World.World world)
        {
            World = world;
            InitializeSystems();
        }

        private void InitializeSystems()
        {
        }

        public void AddSystem<T>(params object[] constructorParams) where T : ISystemBase, new()
        {
            var system = (T) Activator.CreateInstance(typeof(T), constructorParams);

            if (typeof(T).GetInterface("IUpdateSystem") != null)
            {
                _updateSystems.Add((IUpdateSystem) system);
            }
            
            if (typeof(T).GetInterface("IDrawSystem") != null)
            {
                _drawSystems.Add((IDrawSystem) system);
            }
        }
        

        public void Draw(float deltaTime)
        {
            if (!World.IsPrimary) return;

            foreach (IDrawSystem sys in _drawSystems)
                sys.Draw(deltaTime, World.EntityMatcher);
            
        }

        public void Update(float deltaTime)
        {
            if (!World.IsPrimary) return;

            foreach (var entityMatcher in World.EntityMatcher)
            {
                entityMatcher.RunSystems(deltaTime);
            }
            
            foreach (IUpdateSystem sys in _updateSystems)
                sys.Update(deltaTime, World.EntityMatcher);

            World.EntityManager.UpdateEntitiesInStaging();
        }
    }
}
