using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    /// <summary>
    /// Tests checking the possibility of adding game objects to the pool.
    /// </summary>
    public class AddGameObjectsToPoolTests
    {
        /// <summary>
        /// Create a some game object contains a PoolableItem script.
        /// </summary>
        /// <returns>Instantiated game object.</returns>
        private GameObject CreateGameObjectExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>().gameObject;
        }
        
        /// <summary>
        /// Create default Pool Manager instance.
        /// </summary>
        /// <returns>Instantiated Pool Manager.</returns>
        private PoolManager CreatePoolManagerInstance()
        {
            return new GameObject("Objects Pool").AddComponent<PoolManager>();
        }
        
        /// <summary>
        /// The test checks the possibility of adding one game object to the pool.
        /// </summary>
        [Test]
        public void AddSingleGameObjectToPoolTest()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            GameObject gameObject = CreateGameObjectExtendedFromPoolableItem();
            
            // Test Actions
            pool.AddObjectToPool(gameObject);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == 1);
            
            // Clear
            Object.Destroy(pool.gameObject);
            Object.Destroy(gameObject);
        }
        
        /// <summary>
        /// The test checks the possibility of adding multiple game objects to the pool.
        /// </summary>
        [Test]
        public void AddMultipleGameObjectsToPool()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            List<GameObject> gameObjects = new List<GameObject>();
            int objectsCount = Random.Range(10, 100);
            for (int i = 0; i < objectsCount; i++)
            {
                GameObject gameObject = CreateGameObjectExtendedFromPoolableItem();
                gameObjects.Add(gameObject);
            }
            
            // Test Actions
            pool.AddObjectsToPool(gameObjects);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == objectsCount);
            
            // Clear
            Object.Destroy(pool);
            foreach (GameObject gameObject in gameObjects)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}
