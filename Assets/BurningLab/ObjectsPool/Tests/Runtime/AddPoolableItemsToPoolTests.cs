using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    /// <summary>
    /// You are checking the addition of instances of the PoolableItem component to the pool of game objects.
    /// </summary>
    public class AddPoolableItemsToPoolTests
    {
        /// <summary>
        /// The test checks the possibility of adding one instance of the PoolableItem class to the pool of game objects.
        /// </summary>
        [Test]
        public void AddSinglePoolableItemToPoolTest()
        {
            // Create
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            PoolableItem poolableItem = TestUtils.CreatePoolableItemExtendedFromPoolableItem();
            
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
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            List<PoolableItem> poolableItems = new List<PoolableItem>();
            int objectsCount = Random.Range(10, 100);
            for (int i = 0; i < objectsCount; i++)
            {
                PoolableItem poolableItem = TestUtils.CreatePoolableItemExtendedFromPoolableItem();
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
