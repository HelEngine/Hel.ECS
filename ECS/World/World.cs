using Hel.ECS.Entities.Logic;
using Hel.ECS.Entities.Matcher;
using Hel.ECS.Systems.Logic;

namespace Hel.ECS.World
{
    /// <summary>
    /// Worlds are stored within World Managers and contain their own instance of
    /// EntityManager, SystemManager and SpriteBatch. 
    /// </summary>
    public class World
    {
        public string Name { get; }
        public EntityManager EntityManager { get; }
        public SystemManager SystemManager { get; }
        public EntityMatcher EntityMatcher { get; }
        
        public bool IsPrimary;
        public World(string name)
        {
            Name = name;
            EntityManager = new EntityManager(this);
            EntityMatcher = new EntityMatcher(this);
            SystemManager = new SystemManager(this);
        }
    }
}
