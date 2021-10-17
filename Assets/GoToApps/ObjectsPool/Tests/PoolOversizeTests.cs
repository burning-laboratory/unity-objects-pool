using System.Collections.Generic;
using GoToApps.ObjectsPool.Types;
using NUnit.Framework;
using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    /// <summary>
    /// The tests check the possibility of creating new game objects and instances of the PoolableItem component with an empty pool, during the request.
    /// </summary>
    public class PoolOversizeTests
    {
        /// <summary>
        /// Create a some game object contains a PoolableItem script.
        /// </summary>
        /// <returns>Instantiated game object.</returns>
        private GameObject CreateGameObjectExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>().gameObject;
        }
        
        /// <summary>
        /// Create a some game object contains a PoolableItem script.
        /// </summary>
        /// <param name="name">Game object name.</param>
        /// <returns>Game object name.</returns>
        private GameObject CreateGameObjectExtendedFromPoolableItem(string name)
        {
            return new GameObject(name).AddComponent<SomePoolableItem>().gameObject;
        }
        
        /// <summary>
        /// Create a poolable item instance linked to game object.
        /// </summary>
        /// <returns>Instantiated poolable item.</returns>
        private SomePoolableItem CreatePoolableItemExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>();
        }
        
        /// <summary>
        /// Return random prefabs template.
        /// </summary>
        /// <returns>Prefabs template.</returns>
        private List<GameObject> GenerateRandomTemplate()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            int count = Random.Range(10, 50);

            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = CreateGameObjectExtendedFromPoolableItem($"Some Game Object-{i:00}");
                gameObjects.Add(gameObject);
            }
            
            return gameObjects;
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
        /// The test checks the automatic creation of a game object in an empty pool. A positive result is the return
        /// of the created game object from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectsForSinglePrefabMode()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            GameObject prefab = CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
            
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
        /// The test checks the creation of a new instance of the PoolableItem class. A positive result is the return
        /// of the created class from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemsForSinglePrefabMode()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            GameObject prefab = CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
            
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
        /// Checks the automatic creation of multiple game objects with an empty pool. The final result is the return
        /// of many created game objects.
        /// </summary>
        [Test]
        public void CreateMultipleOversizeGameObjectsForSinglePrefabMode()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            GameObject prefab = CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
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
        /// The test checks the creation of multiple instances of the PoolableItem class with an empty pool and the
        /// single prefab filling mode set. A positive result is the return of multiple PoolableItem instances
        /// from the pool.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForSinglePrefabMode()
        {
            // Create
            PoolManager pool = CreatePoolManagerInstance();
            GameObject prefab = CreateGameObjectExtendedFromPoolableItem();
            pool.SetPoolPrefab(prefab);
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
        
        /// <summary>
        /// The test checks the creation of a set of game objects with an empty pool, in the mode of filling with
        /// possible objects with the Create All Objects parameter enabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
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
        /// The test checks the creation of multiple instances of the Poolable Item class with an empty pool, in the
        /// mode of filling a set of prefabs with the Create All Objects parameter disabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetInitializePoolSize(prefabs.Count);

            // Test Actions
            PoolableItem poolableItem = pool.GetItemFromPool();
            GameObject gameObject = poolableItem.gameObject;

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
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
            pool.SetIterationsCount(1);
            
            // Test Actions
            List<GameObject> gameObjects = pool.GetObjectsFromPool(prefabs.Count);

            // Assert
            bool testComplete = true;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (prefabs[i].name + "(Clone)" != gameObjects[i].name) testComplete = false;
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
        /// The test checks the automatic filling of the pool with instances of the PoolableItem class, in the mode of filling with multiple prefabs, with the Create All Objects parameter disabled. A positive result is a multiple instance and a return from a pool of PoolableItem instances.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForMultiplePrefabsModeWithoutCreateAll()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetIterationsCount(1);
            
            // Test Actions
            List<PoolableItem> poolableItems = pool.GetItemsFromPool(prefabs.Count);
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (PoolableItem poolableItem in poolableItems) gameObjects.Add(poolableItem.gameObject);
            pool.AddObjectsToPool(gameObjects);
            
            // Assert
            Assert.True(pool.ItemsCountInPool == prefabs.Count);

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
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetCreateAllPrefabs(true);
            pool.SetIterationsCount(1);

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
        /// The test checks the creation of a new instance of the PoolableItem class in an empty pool in the mode
        /// of filling with multiple prefabs with the Create All Objects parameter enabled.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForMultiplePrefabsModeWithCreateAll()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.MultiplePrefabs);
            pool.SetCreateAllPrefabs(true);
            pool.SetIterationsCount(1);

            // Test Actions
            PoolableItem poolableItem = pool.GetItemFromPool();
            GameObject gameObject = poolableItem.gameObject;
            
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
        /// The test checks the automatic creation of a set of game objects with an empty pool in the mode of filling with a set of game objects when the Create All Objects parameter is enabled.
        /// </summary>
        [Test]
        public void CreateMultipleOversizePoolableItemsForMultiplePrefabsModeWithCreateAllAll()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
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
        
        /// <summary>
        /// The test checks the creation of a set of game objects in an empty pool in the mode of filling with a set
        /// of prefabs with the Create All Objects parameters enabled.
        /// </summary>
        [Test]
        public void CreateMultipleOversizeGameObjectsForMultiplePrefabsModeWithCreateAll()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
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
        /// The test checks the creation of a new game object with an empty pool in the template filling mode.
        /// A positive result is the creation of a new game object and its return from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizeGameObjectForTemplateMode()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
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
        /// The test checks the creation of an instance of the PoolableItem class with an empty pool.
        /// A positive result is the creation of a new instance of the PoolableItem class and its return from the pool.
        /// </summary>
        [Test]
        public void CreateSingleOversizePoolableItemForTemplateMode()
        {
            // Create
            List<GameObject> prefabs = GenerateRandomTemplate();
            PoolManager pool = CreatePoolManagerInstance();
            pool.SetPoolPrefabs(prefabs);
            pool.SetSelfInitializeMode(SelfInitializeMode.InitializeTemplate);
            pool.SetIterationsCount(1);
            
            // Test Actions
            PoolableItem poolableItem = pool.GetItemFromPool();
            GameObject gameObject = poolableItem.gameObject;
            
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
    }
}