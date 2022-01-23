using UnityEngine;

namespace GoToApps.ObjectsPool.Tests
{
    public static class TestUtils
    {
        /// <summary>
        /// Create default Pool Manager instance.
        /// </summary>
        /// <returns>Instantiated Pool Manager.</returns>
        public static PoolManager CreatePoolManagerInstance()
        {
            return new GameObject("Objects Pool").AddComponent<PoolManager>();
        }
        
        /// <summary>
        /// Create a some game object contains a PoolableItem script.
        /// </summary>
        /// <returns>Instantiated game object.</returns>
        public static GameObject CreateGameObjectExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>().gameObject;
        }

        /// <summary>
        /// Create a poolable item instance linked to game object.
        /// </summary>
        /// <returns>Instantiated poolable item.</returns>
        public static SomePoolableItem CreatePoolableItemExtendedFromPoolableItem()
        {
            return new GameObject("Some Game Object").AddComponent<SomePoolableItem>();
        }
    }
}