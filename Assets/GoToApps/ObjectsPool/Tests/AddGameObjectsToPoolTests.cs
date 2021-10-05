using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    public class AddGameObjectsToPoolTests
    {
        private GameObject CreateGameObjectExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>().gameObject;
        }
        
        private PoolManager CreatePoolManagerInstance()
        {
            return new GameObject("Objects Pool").AddComponent<PoolManager>();
        }
        
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
