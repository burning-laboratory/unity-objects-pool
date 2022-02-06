using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    /// <summary>
    /// Tests that verify the receipt of game objects from the pool.
    /// </summary>
    public class GetGameObjectsFromPoolTests
    {
        /// <summary>
        /// The test checks the possibility of obtaining one game object from a filled game pool.
        /// </summary>
        [Test]
        public void GetSingleGameObjectFromPoolTest()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            GameObject gameObject = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            pool.AddObjectToPool(gameObject);
            
            // Test Actions
            GameObject gameObjectFromPool = pool.GetObjectFromPool();
            
            // Assert
            int gameObjectInstanceId = gameObject.GetInstanceID();
            int gameObjectFromPoolInstanceId = gameObjectFromPool.GetInstanceID();
            Assert.True(gameObjectInstanceId == gameObjectFromPoolInstanceId);
            
            // Clear
            Object.Destroy(pool);
            Object.Destroy(gameObjectFromPool);
            Object.Destroy(gameObject);
        }
        
        /// <summary>
        /// Test checks the possibility of obtaining a set of game objects from a filled pool.
        /// </summary>
        [Test]
        public void GetMultipleGameObjectsFromPoolTest()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            int objectsCount = Random.Range(10, 100);
            for (int i = 0; i < objectsCount; i++)
            {
                GameObject gameObject = TestUtils.CreateGameObjectExtendedFromPoolableItem();
                pool.AddObjectToPool(gameObject);
            }
            
            // Test Action
            List<GameObject> gameObjectsFromPool = pool.GetObjectsFromPool(objectsCount);
            
            // Assert
            Assert.True(objectsCount == gameObjectsFromPool.Count);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject gameObject in gameObjectsFromPool)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}