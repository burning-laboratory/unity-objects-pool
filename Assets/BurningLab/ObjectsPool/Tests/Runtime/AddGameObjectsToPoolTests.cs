using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    /// <summary>
    /// Tests checking the possibility of adding game objects to the pool.
    /// </summary>
    public class AddGameObjectsToPoolTests
    {
        /// <summary>
        /// The test checks the possibility of adding one game object to the pool.
        /// </summary>
        [Test]
        public void AddSingleGameObjectToPoolTest()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            GameObject gameObject = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            
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
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            List<GameObject> gameObjects = new List<GameObject>();
            int objectsCount = Random.Range(10, 100);
            for (int i = 0; i < objectsCount; i++)
            {
                GameObject gameObject = TestUtils.CreateGameObjectExtendedFromPoolableItem();
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
