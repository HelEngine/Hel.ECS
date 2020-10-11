using System;
using System.Collections.Generic;
using Hel.ECS.Components.Container;
using Hel.ECS.Components.Model;
using Hel.ECS.Systems.System;
using Hel.Toolkit.DataStructure.Arrays;

namespace Hel.ECS.Entities.Matcher.Groups
{
    public class EntityGroup<TComponent1, TComponent2> : IEntityGroup <TComponent1, TComponent2>
        where TComponent1 : struct, IComponent
        where TComponent2 : struct, IComponent
    {
        private ComponentContainer Component1 { get; }
        private ComponentContainer Component2 { get; }
        private DynamicArray<bool> Entities { get; }

        private delegate (TComponent1?, TComponent2?) SystemMethods(float deltaTime, int id,
            TComponent1 component, TComponent2 component2);

        private event SystemMethods RunSystemEvents;
        
        private int _entityCounter;
        
        public EntityGroup(Dictionary<Type, ComponentContainer> componentContainer)
        {
            Entities = new DynamicArray<bool>(256);

            componentContainer.TryGetValue(typeof(TComponent1), out var component1);
            Component1 = component1;
            Component1!.OnEntitiesModified += OnEntitiesChanged;
            
            componentContainer.TryGetValue(typeof(TComponent2), out var component2);
            Component2 = component2;
            Component2!.OnEntitiesModified += OnEntitiesChanged;
           
        }

        public void OnEntitiesChanged(int[] entitiesList, ComponentContainer.EntityChangeDirection changeDirection)
        {
            switch (changeDirection)
            {
                case ComponentContainer.EntityChangeDirection.Removed:
                    RemoveEntities(entitiesList);
                    break;
                case ComponentContainer.EntityChangeDirection.Added:
                    AddEntities(entitiesList);
                    break;
            }
        }

        private void RemoveEntities(int[] entitiesList)
        {
            for (int i = 0; i < entitiesList.Length; i++)
            {
                Entities[entitiesList[i]] = false;
            }
        }

        private void AddEntities(int[] entitiesList)
        {
            for (int i = 0; i < entitiesList.Length; i++)
            {
                var compIndex = entitiesList[i];
                if (Component1[compIndex] == default) continue;
                if (Component2[compIndex] == default) continue;
                Entities[compIndex] = true;
            }
        }

        public void AddSystem(ISystem<TComponent1, TComponent2> system)
        {
            RunSystemEvents += system.RunSystem;
        }
        
        public void AddSystems(IEnumerable<ISystem<TComponent1, TComponent2>> systems)
        {
            foreach (var system in systems)
            {
                AddSystem(system);
            }
        }
        
        public void RunSystems(float deltaTime)
        {
            for (_entityCounter = 0; _entityCounter < Entities.Size; _entityCounter++)
            {
                if (Entities[_entityCounter])
                {
                    var entityData = RunSystemEvents?.Invoke(deltaTime, _entityCounter, (TComponent1) Component1[_entityCounter], (TComponent2) Component2[_entityCounter]);

                    UpdateValues(entityData);
                }
            }
        }

        private void UpdateValues((TComponent1?, TComponent2?)? values)
        {
            if (!values.HasValue) return; 
            if (values.Value.Item1.HasValue) Component1.UpdateComponent(_entityCounter, values.Value.Item1.Value);
            if (values.Value.Item2.HasValue) Component2.UpdateComponent(_entityCounter, values.Value.Item2.Value);
        }
        
        public void ForEach(Func<int, TComponent1, TComponent2, (TComponent1?, TComponent2?)?> func)
        {
            for (int i = 0; i < Entities.Size; i++)
            {
                if (Entities[i])
                {
                    UpdateValues(func(i, (TComponent1) Component1[i], (TComponent2) Component2[i]));
                }
            }
        }
    }
}