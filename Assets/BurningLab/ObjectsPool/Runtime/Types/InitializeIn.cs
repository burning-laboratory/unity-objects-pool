namespace BurningLab.ObjectsPool.Types
{
    /// <summary>
    /// Select method for initialize game objects pool.
    /// </summary>
    [System.Serializable]
    public enum InitializeIn
    {
        /// <summary>
        /// Initialize pool in Awake event method.
        /// </summary>
        Awake = 0,
        
        /// <summary>
        /// Initialize pool in Start event method.
        /// </summary>
        Start = 1,
        
        /// <summary>
        /// Initialize in Initialize method calling.
        /// </summary>
        Custom = 2
    }
}