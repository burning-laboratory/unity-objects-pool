using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    public class AddPoolableItemsToPoolTests
    {
        private SomePoolableItem CreatePoolableItemExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>();
        }
        
        private PoolManager CreatePoolManagerInstance()
        {
            return new GameObject("Objects Pool").AddComponent<PoolManager>();
        }

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
