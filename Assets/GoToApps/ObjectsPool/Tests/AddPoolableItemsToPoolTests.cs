using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    /// <summary>
    /// You are checking the addition of instances of the PoolableItem component to the pool of game objects.
    /// </summary>
    public class AddPoolableItemsToPoolTests
    {
        /// <summary>
        /// Create a poolable item instance linked to game object.
        /// </summary>
        /// <returns>Instantiated poolable item.</returns>
        private SomePoolableItem CreatePoolableItemExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>();
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
        /// The test checks the possibility of adding one instance of the PoolableItem class to the pool of game objects.
        /// </summary>
        [Test]
        public void AddSinglePoolableItemToPoolTest()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            PoolableItem poolableItem = CreatePoolableItemExtendedFromPoolableItem();
            
            // Test Actions
            pool.AddItemToPool(poolableItem);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == 1);
            
            // Clear
            Object.Destroy(pool);
            Object.Destroy(poolableItem);
        }
        
        /// <summary>
        /// The test checks the possibility of adding multiple instances of the PoolableItem class to the pool.
        /// </summary>
        [Test]
        public void AddMultiplePoolableItemsToPool()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            List<PoolableItem> poolableItems = new List<PoolableItem>();
            int objectsCount = Random.Range(10, 100);
            for (int i = 0; i < objectsCount; i++)
            {
                PoolableItem poolableItem = CreatePoolableItemExtendedFromPoolableItem();
                poolableItems.Add(poolableItem);
            }
            
            // Test Actions
            pool.AddItemsToPool(poolableItems);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == objectsCount);
            
            // Clear
            Object.Destroy(pool);
            foreach (PoolableItem poolableItem in poolableItems)
            {
                Object.Destroy(poolableItem);
            }
        }
    }
}
