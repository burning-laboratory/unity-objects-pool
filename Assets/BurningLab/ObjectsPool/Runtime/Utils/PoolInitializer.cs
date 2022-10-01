using BurningLab.ObjectsPool.Exceptions;
using UnityEngine;

namespace BurningLab.ObjectsPool.Utils
{
    /// <summary>
    /// Initialize pool utils.
    /// </summary>
    public static class PoolInitializer
    {
        /// <summary>
        /// Create poolable game object.
        /// </summary>
        /// <param name="prefab">Poolable prefab.</param>
        /// <returns>Instantiated game object.</returns>
        public static PoolableItem CreateGameObject(GameObject prefab, Transform parent, PoolManager pool)
        {
            GameObject instantiatedGm = Object.Instantiate(prefab, parent);
            if (instantiatedGm.TryGetComponent(out PoolableItem poolableItem))
            {
                poolableItem.SetParentPool(pool);
                poolableItem.gameObject.SetActive(false);
                return poolableItem;   
            }
            throw new ObjectsPoolException("Not found a poolable item component. \n Please create a child class from PoolableItem and add to prefab GameObject.");
        }
    }
}