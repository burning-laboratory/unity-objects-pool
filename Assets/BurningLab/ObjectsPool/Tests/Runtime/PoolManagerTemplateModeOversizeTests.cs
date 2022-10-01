using System.Collections.Generic;
using BurningLab.ObjectsPool.Types;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    /// <summary>
    /// Pool manager oversize tests with template self initialization mode.
    /// </summary>
    public class PoolManagerTemplateModeOversizeTests
    {
         /// <summary>
        /// The test checks the creation of a new game object with an empty pool in the template filling mode.
        /// A positive result is the creation of a new game object and its return from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectForTemplateMode()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
            pool.SetCreateOversizePrefabs(true);
            pool.SetIterationsCount(1);
            
            // Test Actions
            GameObject gameObject = pool.GetObjectFromPool();
            
            // Assert
            Assert.True(prefabs[0].name + "(Clone)" == gameObject.name);

            // Clear
            Object.Destroy(pool);
            Object.Destroy(gameObject);
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
         
        /// <summary>
        /// The test checks the creation of a set of game objects with an empty pool in the mode of filling the pool with a given sequence of game objects.
        /// During the test, a random number of objects is requested from an empty pool. A positive result is the creation and return of the requested number of objects.
        /// </summary>
         [Test]
         public void CreateMultipleOversizeGameObjectsForTemplateMode()
         {
             // Create
             List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
             PoolManager pool = TestUtils.CreatePoolManagerInstance();
             pool.SetPoolPrefabs(prefabs);
             pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
             pool.SetCreateOversizePrefabs(true);
             pool.SetIterationsCount(1);
             int count = Random.Range(0, 25);
            
             // Test Actions
             List<GameObject> gameObjectsFromPool = pool.GetObjectsFromPool(count);
            
             // Assert
             Assert.True(gameObjectsFromPool.Count == count);

             // Clear
             Object.Destroy(pool);
             foreach (GameObject gameObjectFromPool in gameObjectsFromPool) Object.Destroy(gameObjectFromPool);
             foreach (GameObject prefab in prefabs) Object.Destroy(prefab);
         }
         
        /// <summary>
        /// The test checks the creation of an instance of the PoolableItem class with an empty pool.
        /// A positive result is the creation of a new instance of the PoolableItem class and its return from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForTemplateMode()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
            pool.SetCreateOversizePrefabs(true);
            pool.SetIterationsCount(1);
            
            // Test Actions
            PoolableItem poolableItem = pool.GetItemFromPool();
            GameObject gameObject = poolableItem.gameObject;
            
            // Assert
            Assert.True(prefabs[0].name + "(Clone)" == gameObject.name);

            // Clear
            Object.Destroy(pool);
            Object.Destroy(poolableItem);
            Object.Destroy(gameObject);
            foreach (GameObject prefab in prefabs) Object.Destroy(prefab);
        }
        
        /// <summary>
        /// The test checks the creation of multiple instances of the PoolableItem component with an empty pool in the pool filling mode with a specified sequence of game objects.
        /// During the test, a random number of objects is requested from an empty pool. A positive result is the creation and return of the requested number of objects.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForTemplateMode()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
            pool.SetIterationsCount(1);
            pool.SetCreateOversizePrefabs(true);
            int count = Random.Range(0, 25);
            
            // Test Actions
            List<PoolableItem> poolableItems = pool.GetItemsFromPool(count);

            // Assert
            Assert.True(poolableItems.Count == count);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject prefab in prefabs) Object.Destroy(prefab);
            foreach (PoolableItem poolableItem in poolableItems) Object.Destroy(poolableItem);
        }
    }
}