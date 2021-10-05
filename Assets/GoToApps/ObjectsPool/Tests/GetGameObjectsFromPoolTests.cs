using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    public class GetGameObjectsFromPoolTests
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