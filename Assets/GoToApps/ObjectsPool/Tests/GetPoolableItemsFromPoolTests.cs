using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    public class GetPoolableItemsFromPoolTests
    {
        private SomePoolableItem CreateSomePoolableItemExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>();
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
            PoolableItem poolableItem = CreateSomePoolableItemExtendedFromPoolableItem();
            pool.AddItemToPool(poolableItem);
            
            // Test Actions
            PoolableItem poolableItemFromPool = pool.GetItemFromPool();
            
            // Assert
            int poolableItemInstanceId = poolableItem.GetInstanceID();
            int poolableItemFromPoolInstanceId = poolableItemFromPool.GetInstanceID();
            Assert.True(poolableItemInstanceId == poolableItemFromPoolInstanceId);
            
            // Clear
            Object.Destroy(pool);
            Object.Destroy(poolableItemFromPool);
            Object.Destroy(poolableItem);
        }
        
        [Test]
        public void GetMultipleGameObjectsFromPoolTest()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            int itemsCount = Random.Range(10, 100);
            for (int i = 0; i < itemsCount; i++)
            {
                PoolableItem poolableItem = CreateSomePoolableItemExtendedFromPoolableItem();
                pool.AddItemToPool(poolableItem);
            }
            
            // Test Action
            List<PoolableItem> poolableItemsFromPool = pool.GetItemsFromPool(itemsCount);
            
            // Assert
            Assert.True(itemsCount == poolableItemsFromPool.Count);

            // Clear
            Object.Destroy(pool);
            foreach (PoolableItem poolableItem in poolableItemsFromPool)
            {
                Object.Destroy(poolableItem);
            }
        }
    }
}