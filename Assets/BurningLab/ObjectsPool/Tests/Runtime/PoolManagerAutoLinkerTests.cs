using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    /// <summary>
    /// Pool manager automatic item on add to pool linker.
    /// </summary>
    public class PoolManagerAutoLinkerTests
    {
        /// <summary>
        /// The test checks the automatic linking of the game object when it is added to the pool.
        /// A positive result is the binding of the added object to the pool to which it was added.
        /// </summary>
        [Test]
        public void AddSingleGameObjectToPoolAutoSetParentPoolTest()
        {
            // Create
            GameObject gameObject = TestUtils.CreateGameObjectExtendedFromPoolableItem();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(false);

            // Test Actions
            pool.AddObjectToPool(gameObject);
            PoolableItem item = pool.GetItemFromPool();
            
            // Assert
            PoolManager parentPool = item.ParentPool;
            Assert.True(parentPool.gameObject.name == pool.gameObject.name);

            // Clear
            Object.Destroy(pool);
            Object.Destroy(gameObject);
            Object.Destroy(item);
        }
        
        /// <summary>
        /// The test checks the automatic linking of many game objects when added to the pool.
        /// A positive result is the binding of all added game objects to the pool to which they were added.
        /// </summary>
        [Test]
        public void AddMultipleGameObjectsToPoolAutoSetParentPoolTest()
        {
            // Create
            List<GameObject> gameObjects = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(false);

            // Test Actions
            pool.AddObjectsToPool(gameObjects);
            List<PoolableItem> items = pool.GetItemsFromPool(gameObjects.Count);
            
            // Assert
            bool testComplete = true;
            foreach (PoolableItem item in items)
            {
                if (item.ParentPool.gameObject.name != pool.gameObject.name) testComplete = false;
            }
            Assert.True(testComplete);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject gameObject in gameObjects) Object.Destroy(gameObject);
            foreach (PoolableItem item in items) Object.Destroy(item);
        }
        
        /// <summary>
        /// The test checks the automatic linking of the PoolableItem component instance when it is added to the pool.
        /// A positive result is the binding of the added object to the pool to which it was added.
        /// </summary>
        [Test]
        public void AddSinglePoolableItemToPoolAutoSetParentPoolTest()
        {
            // Create
            PoolableItem itemTemplate = TestUtils.CreatePoolableItemExtendedFromPoolableItem();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(false);

            // Test Actions
            pool.AddItemToPool(itemTemplate);
            PoolableItem item = pool.GetItemFromPool();
            
            // Assert
            PoolManager parentPool = item.ParentPool;
            Assert.True(parentPool.gameObject.name == pool.gameObject.name);

            // Clear
            Object.Destroy(pool);
            Object.Destroy(itemTemplate);
            Object.Destroy(item);
        }
        
        /// <summary>
        /// The test checks the automatic linking of multiple instances of the PoolableItem component when added to the pool.
        /// A positive result is the binding of all added objects to the pool to which they were added.
        /// </summary>
        [Test]
        public void AddMultiplePoolableItemsToPoolAutoSetParentPoolTest()
        {
            // Create
            List<GameObject> gameObjects = TestUtils.GenerateRandomTemplate();
            List<PoolableItem> poolableItems = new List<PoolableItem>();
            foreach (GameObject gameObject in gameObjects)
            {
                PoolableItem poolableItem = gameObject.GetComponent<PoolableItem>();
                poolableItems.Add(poolableItem);
            }
            
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(false);

            // Test Actions
            pool.AddItemsToPool(poolableItems);
            List<PoolableItem> items = pool.GetItemsFromPool(poolableItems.Count);
            
            // Assert
            bool testComplete = true;
            foreach (PoolableItem item in items)
            {
                if (item.ParentPool.gameObject.name != pool.gameObject.name) testComplete = false;
            }
            Assert.True(testComplete);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject gameObject in gameObjects) Object.Destroy(gameObject);
            foreach (PoolableItem item in items) Object.Destroy(item);
            foreach (PoolableItem poolableItem in poolableItems) Object.Destroy(poolableItem);
        }
    }
}