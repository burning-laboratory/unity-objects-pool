using UnityEngine;

namespace BurningLab.ObjectsPool
{
    /// <summary>
    /// Poolable item abstract wrapper.
    /// </summary>
    public abstract class PoolableItem : MonoBehaviour
    {
        private PoolManager _pool;
        
        /// <summary>
        /// Return reference to linked pool.
        /// </summary>
        public PoolManager ParentPool => _pool;
        
        /// <summary>
        /// Set parent object pool.
        /// </summary>
        /// <param name="pool">Pool manager</param>
        public void SetParentPool(PoolManager pool) => _pool = pool;
        
        /// <summary>
        /// Return game object to parent pool.
        /// </summary>
        public void ReturnToPool() => _pool.AddItemToPool(this);
    }
}