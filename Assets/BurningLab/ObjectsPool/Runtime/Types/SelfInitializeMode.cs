namespace BurningLab.ObjectsPool.Types
{
    /// <summary>
    /// Self initialize type.
    /// </summary>
    public enum SelfInitializeMode
    {
        /// <summary>
        /// Add initialized single prefabs.
        /// </summary>
        SinglePrefab = 0,
        
        /// <summary>
        /// Initialize multiple prefabs and add to pool.
        /// </summary>
        MultiplePrefabs = 1,
        
        /// <summary>
        /// Initialize template from list.
        /// Prefabs from the list will be added to the pool in the same order.
        /// </summary>
        InitializeTemplate = 2,
    }
}