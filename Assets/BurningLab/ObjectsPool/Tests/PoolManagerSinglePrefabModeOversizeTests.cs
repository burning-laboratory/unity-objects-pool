using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    /// <summary>
    /// Pool manager single prefab mode test set.
    /// </summary>
    public class PoolManagerSinglePrefabModeOversizeTests
    {
        /// <summary>
        /// The test checks the automatic creation of a game object in an empty pool. A positive result is the return
        /// of the created game object from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectForSinglePrefabMode()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            GameObject prefab = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
            pool.SetCreateOversizePrefabs(true);
            
            // Test Actions
            GameObject gm = pool.GetObjectFromPool();
            gm.GetComponent<PoolableItem>().ReturnToPool();
            
            // Assert
            Assert.True(pool.ItemsCountInPool == 1);
            
            // Clear
            Object.Destroy(pool);
            Object.Destroy(gm);
        }
        
        /// <summary>
        /// Checks the automatic creation of multiple game objects with an empty pool. The final result is the return
        /// of many created game objects.
        /// </summary>
        [Test]
        public void CreateMultipleOversizeGameObjectsForSinglePrefabMode()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            GameObject prefab = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
            pool.SetCreateOversizePrefabs(true);
            int count = Random.Range(0, 50);
            
            // Test Actions
            List<GameObject> gameObjects = pool.GetObjectsFromPool(count);
            pool.AddObjectsToPool(gameObjects);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == count);
            
            // Clear
            Object.Destroy(pool);
            foreach (GameObject gameObject in gameObjects)
            {
                Object.Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// The test checks the creation of a new instance of the PoolableItem class. A positive result is the return
        /// of the created class from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForSinglePrefabMode()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            GameObject prefab = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
            pool.SetCreateOversizePrefabs(true);
            
            // Test Actions
            PoolableItem item = pool.GetItemFromPool();
            item.ReturnToPool();
            
            // Assert
            Assert.True(pool.ItemsCountInPool == 1);
            
            // Clear
            Object.Destroy(pool);
            Object.Destroy(item);
        }

        /// <summary>
        /// The test checks the creation of multiple instances of the PoolableItem class with an empty pool and the
        /// single prefab filling mode set. A positive result is the return of multiple PoolableItem instances
        /// from the pool.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForSinglePrefabMode()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            GameObject prefab = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
            pool.SetCreateOversizePrefabs(true);
            int count = Random.Range(0, 50);
            
            // Test Actions
            List<PoolableItem> poolableItems = pool.GetItemsFromPool(count);
            pool.AddItemsToPool(poolableItems);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == count);
            
            // Clear
            Object.Destroy(pool);
            foreach (PoolableItem item in poolableItems)
            {
                Object.Destroy(item);
            }
        }
    }
}