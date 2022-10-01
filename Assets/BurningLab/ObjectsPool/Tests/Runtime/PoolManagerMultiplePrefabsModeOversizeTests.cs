using System.Collections.Generic;
using BurningLab.ObjectsPool.Types;
using NUnit.Framework;
using UnityEngine;

namespace BurningLab.ObjectsPool.Tests
{
    public class PoolManagerMultiplePrefabsModeOversizeTests
    {
        /// <summary>
        /// The test checks the creation of a set of game objects with an empty pool, in the mode of filling with
        /// possible objects with the Create All Objects parameter enabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetInitializePoolSize(prefabs.Count);

            // Test Actions
            GameObject gameObject = pool.GetObjectFromPool();

            // Assert
            Assert.True(prefabs.Exists(g => g.name + "(Clone)" == gameObject.name));

            // Clear
            Object.Destroy(pool);
            Object.Destroy(gameObject);
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
        
        /// <summary>
        /// The test checks the creation of a set of game objects with an empty pool in the mode of filling with a set of prefabs with the Create All Objects parameter disabled.
        /// </summary>
        [Test]
        public void CreateMultipleOversizeGameObjectsForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);

            // Test Actions
            List<GameObject> gameObjects = pool.GetObjectsFromPool(prefabs.Count);

            // Assert
            pool.AddObjectsToPool(gameObjects);
            Assert.True(gameObjects.Count == prefabs.Count);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject gm in gameObjects)
            {
                Object.Destroy(gm);
            }
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
        
        /// <summary>
        /// The test checks the creation of a new game object in an empty pool when filling with multiple
        /// objects with the Create All Objects parameter disabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectForMultiplePrefabsModeWithCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetCreateAllPrefabs(true);
            pool.SetIterationsCount(1);

            // Test Actions
            List<GameObject> gameObjects = pool.GetObjectsFromPool(prefabs.Count);

            // Assert
            bool testComplete = true;
            foreach (GameObject prefab in prefabs)
            {
                if (gameObjects.Exists(gm => gm.name == prefab.name + "(Clone)") == false)
                {
                    testComplete = false;
                    Debug.Log("Not found gm: " + prefab.name + "(Clone)");
                }
            }
            Assert.True(testComplete);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
        
        /// <summary>
        /// The test checks the creation of a set of game objects in an empty pool in the mode of filling with a set
        /// of prefabs with the Create All Objects parameters enabled.
        /// </summary>
        [Test]
        public void CreateMultipleOversizeGameObjectsForMultiplePrefabsModeWithCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetCreateAllPrefabs(true);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetIterationsCount(1);

            // Test Actions
            List<GameObject> gameObjects = pool.GetObjectsFromPool(prefabs.Count);

            // Assert
            bool testComplete = true;
            foreach (GameObject prefab in prefabs)
            {
                if (gameObjects.Exists(gm => gm.name == prefab.name + "(Clone)") == false)
                {
                    testComplete = false;
                    Debug.Log("Not found gm: " + prefab.name);
                }
                
            }
            Assert.True(testComplete);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject gm in gameObjects)
            {
                Object.Destroy(gm);
            }
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
        
        /// <summary>
        /// The test checks the creation of multiple instances of the Poolable Item class with an empty pool, in the
        /// mode of filling a set of prefabs with the Create All Objects parameter disabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetInitializePoolSize(prefabs.Count);

            // Test Actions
            PoolableItem poolableItem = pool.GetItemFromPool();

            // Assert
            GameObject gameObject = poolableItem.gameObject;
            Assert.True(prefabs.Exists(g => g.name + "(Clone)" == gameObject.name));

            // Clear
            Object.Destroy(pool);
            Object.Destroy(gameObject);
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }

        /// <summary>
        /// The test checks the automatic filling of the pool with instances of the PoolableItem class, in the mode of filling with multiple prefabs, with the Create All Objects parameter disabled. A positive result is a multiple instance and a return from a pool of PoolableItem instances.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);

            // Test Actions
            List<PoolableItem> poolableItems = pool.GetItemsFromPool(prefabs.Count);

            // Assert
            pool.AddItemsToPool(poolableItems);
            Assert.True(pool.ItemsCountInPool == prefabs.Count);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }

        /// <summary>
        /// The test checks the creation of a new instance of the PoolableItem class in an empty pool in the mode
        /// of filling with multiple prefabs with the Create All Objects parameter enabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForMultiplePrefabsModeWithCreateAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetCreateAllPrefabs(true);
            pool.SetIterationsCount(1);

            // Test Actions
            PoolableItem poolableItem = pool.GetItemFromPool();

            // Assert
            GameObject gameObject = poolableItem.gameObject;
            Assert.True(prefabs.Exists(g => g.name + "(Clone)" == gameObject.name));

            // Clear
            Object.Destroy(pool);
            Object.Destroy(gameObject);
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
        
        /// <summary>
        /// The test checks the automatic creation of a set of game objects with an empty pool in the mode of filling with a set of game objects when the Create All Objects parameter is enabled.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForMultiplePrefabsModeWithCreateAllAll()
        {
            // Create
            List<GameObject> prefabs = TestUtils.GenerateRandomTemplate();
            PoolManager pool = TestUtils.CreatePoolManagerInstance();
            pool.SetCreateOversizePrefabs(true);
            pool.SetPoolPrefabs(prefabs);
            pool.SetCreateAllPrefabs(true);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetIterationsCount(1);
            
            // Test Actions
            List<PoolableItem> poolableItems = pool.GetItemsFromPool(prefabs.Count);
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (PoolableItem poolableItem in poolableItems) gameObjects.Add(poolableItem.gameObject);
            
            // Assert
            bool testComplete = true;
            foreach (GameObject prefab in prefabs)
            {
                if (gameObjects.Exists(gm => gm.name == prefab.name + "(Clone)") == false)
                {
                    testComplete = false;
                    Debug.Log("Not found gm: " + prefab.name + "(Clone)");
                }
            }
            Assert.True(testComplete);

            // Clear
            Object.Destroy(pool);
            foreach (GameObject gm in gameObjects)
            {
                Object.Destroy(gm);
            }
            foreach (GameObject prefab in prefabs)
            {
                Object.Destroy(prefab);
            }
        }
    }
}