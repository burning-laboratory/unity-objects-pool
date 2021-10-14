using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    /// <summary>
    /// Tests that verify the receipt of game objects from the pool.
    /// </summary>
    public class GetGameObjectsFromPoolTests
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
        /// The test checks the possibility of obtaining one game object from a filled game pool.
        /// </summary>
        [Test]
        public void GetSingleGameObjectFromPoolTest()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            GameObject gameObject = CreateGameObjectExtendedFromPoolableItem();
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
            PoolManager pool = CreatePoolManagerInstance();
            int objectsCount = Random.Range(10, 100);
            for (int i = 0; i < objectsCount; i++)
            {
                GameObject gameObject = CreateGameObjectExtendedFromPoolableItem();
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