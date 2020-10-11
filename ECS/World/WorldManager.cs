using System.Collections.Generic;
using Hel.ECS.Exceptions;

namespace Hel.ECS.World
{
    internal interface IWorldManager
    {
        /// <summary>
        /// Generates a new world and stores it in the world manager.
        /// The newly generated world will also generate a SystemManager and an EntityManager.
        /// </summary>
        /// <param name="spriteBatch">Game1 spritebatch</param>
        /// <param name="worldName">The name you want this world to be stored with in the dictionary</param>
        /// <returns></returns>
        World GenerateWorld(string worldName);
        /// <summary>
        /// Gets a world from the dictionary by name and returns it.
        /// </summary>
        /// <param name="worldName">The name of the world to return</param>
        /// <returns></returns>
        World GetWorld(string worldName);

        /// <summary>
        /// Sets the primary World. The world provides must exist in the worlds dictionary.
        /// Throws <see cref="InvalidWorldException"/>
        /// </summary>
        /// <param name="worldName"></param>
        void SetPrimaryWorld(string worldName);

    }

    /// <summary>
    /// The world manager contains references to all the instantiated worlds in your game. 
    /// </summary>
    public class WorldManager : IWorldManager
    {
       private readonly Dictionary<string, World> _worlds = new Dictionary<string, World>();
       public World PrimaryWorld { get; private set; }

       public WorldManager()
       { }

       public World GenerateWorld(string worldName)
       {
           World world = new World(worldName);
           _worlds.Add(worldName, world);

           PrimaryWorld ??= world;

           return world;
       }

       public void SetPrimaryWorld(string worldName)
       {
           PrimaryWorld.IsPrimary = false;
           var newPrimaryWorld = _worlds.ContainsKey(worldName)
               ? _worlds[worldName]
               : throw new InvalidWorldException($"{worldName} does not exist. Can not set as primary world.");
           newPrimaryWorld.IsPrimary = true;
           PrimaryWorld = newPrimaryWorld;
       }

       public void Draw(float deltaTime)
       {
           PrimaryWorld.SystemManager.Draw(deltaTime);
       }

       public void Update(float deltaTime)
       {
           PrimaryWorld.SystemManager.Update(deltaTime);
       }

       public World GetWorld(string worldName)
       {
           return _worlds.ContainsKey(worldName) ? _worlds[worldName] : null;
       }
    }
}
