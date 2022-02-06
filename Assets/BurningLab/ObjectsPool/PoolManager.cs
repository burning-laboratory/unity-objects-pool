using System;
using System.Collections.Generic;
using System.Linq;
using BurningLab.ObjectsPool.Exceptions;
using BurningLab.ObjectsPool.Types;
using BurningLab.ObjectsPool.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BurningLab.ObjectsPool
{
    /// <summary>
    /// Objects pool manager.
    /// </summary>
    [AddComponentMenu("Burning-Lab/Objects Pool/Pool Manager")]
    public class PoolManager : MonoBehaviour
    {
        [Tooltip("Parent game object transform for game objects in pool.")]
        [SerializeField] private Transform _poolParentTransform;

        [Tooltip("Specifies whether to create additional game objects if the pool is empty at the time of the request for the game object.")]
        [SerializeField] private bool _createOversizePrefabs;
        
        [Tooltip("The flag responsible for marking the `DontDestroyOnLoad` object. When the scene is restarted, the duplicate object will be deleted.")]
        [SerializeField] private bool _dontDestroyOnLoad;

        [Tooltip("The key by which the pool manager will save data. We recommend using unique keys for each instance of the pool manager.")]
        [SerializeField] private string _poolManagerDataPlayerPrefsKey;
        
        [Tooltip("Automatic pool initialize.")]
        [SerializeField] private bool _selfInitialize;
        
        [Tooltip("Set initialize pool method.")]
        [SerializeField] private InitializeIn _initializeIn;
        
        [Tooltip("Type of self pool initialization.")]
        [SerializeField] private SelfInitializeMode _initializeMode;

        [Tooltip("Game object prefab.")]
        [SerializeField] private GameObject _poolPrefab;
        
        [Tooltip("Prefabs list.")]
        [SerializeField] private List<GameObject> _prefabs;

        [Tooltip("Set initialize game objects count.")]
        [SerializeField] private int _initializePoolSize;
        
        [Tooltip("The number of passes through the list during initialization.")]
        [SerializeField] private int _iterationsCount;

        [Tooltip("The need to create each object from the list. Enabling this parameter ensures that after initialization, all the objects from the list above will be in the pool.")]
        [SerializeField] private bool _createAllObjects;

        [Tooltip("Show debug logs.")]
        [SerializeField] private bool _showDebugLogs;
        
        [Tooltip("Displays a message with the initialization time of the pool.")]
        [SerializeField] private bool _showPoolInitializerLogs;
        
        [Tooltip("Displays operations with pool objects in the logs.")]
        [SerializeField] private bool _showPoolOperationLogs;

        [Tooltip("Responsible for outputting logs of the background instance monitoring module. `ContDestroyOnLoad`.")] 
        [SerializeField] private bool _showBackgroundControlLogs;

        private readonly Queue<PoolableItem> _pool = new Queue<PoolableItem>();
        
        private int _lastInitializedItemFromTemplateIndex;
        private bool _initialized;
        private int _instanceId;
        
        /// <summary>
        /// Return items count in pool.
        /// </summary>
        public int ItemsCountInPool => _pool.Count;
        
        /// <summary>
        /// A calculated property that returns a prefab for creating a new pool object, taking into account all settings.
        /// </summary>
        private GameObject PoolPrefab
        {
            get
            {
                switch (_initializeMode)
                {
                    case SelfInitializeMode.SinglePrefab: return _poolPrefab;
                    
                    case SelfInitializeMode.MultiplePrefabs:
                        if (_createAllObjects)
                        {
                            GameObject nextPositionInQueuePrefab = _prefabs[_lastInitializedItemFromTemplateIndex];
                            _lastInitializedItemFromTemplateIndex++;
                            if (_lastInitializedItemFromTemplateIndex == _prefabs.Count)
                            {
                                _lastInitializedItemFromTemplateIndex = 0;
                            }
                            return nextPositionInQueuePrefab;
                        }
                        else
                        {
                            int i = Random.Range(0, _prefabs.Count);
                            return _prefabs[i];
                        }

                    case SelfInitializeMode.InitializeTemplate:
                        GameObject prefabForInitializeTemplate = _prefabs[_lastInitializedItemFromTemplateIndex];
                        _lastInitializedItemFromTemplateIndex++;
                        if (_lastInitializedItemFromTemplateIndex == _prefabs.Count)
                        {
                            _lastInitializedItemFromTemplateIndex = 0;
                        }
                        return prefabForInitializeTemplate;
                }
                return _poolPrefab;
            }  
        }
        
        /// <summary>
        /// The number of objects to be created during initialization. Calculated based on the settings.
        /// </summary>
        private int InitializeIterationsCount
        {
            get
            {
                switch (_initializeMode)
                {
                    case SelfInitializeMode.SinglePrefab: return _initializePoolSize;
                    case SelfInitializeMode.MultiplePrefabs:
                        if (_createAllObjects) return _prefabs.Count * _iterationsCount;
                        else return _initializePoolSize;
                    case SelfInitializeMode.InitializeTemplate: return _iterationsCount * _prefabs.Count;
                }

                return 0;
            }
        }
        
        /// <summary>
        /// Initialize pool.
        /// </summary>
        private void InitializePool()
        {
            DateTime startDt = DateTime.Now;

            List<PoolableItem> poolableItems = new List<PoolableItem>();
            for (int i = 0; i < InitializeIterationsCount; i++)
            {
                PoolableItem poolableItem = PoolInitializer.CreateGameObject(PoolPrefab, _poolParentTransform, this);
                poolableItems.Add(poolableItem);
            }

            if (_initializeMode == SelfInitializeMode.MultiplePrefabs && _createAllObjects) poolableItems = ArrayUtils.MixArray(poolableItems);

            foreach (PoolableItem poolableItem in poolableItems)
            {
                _pool.Enqueue(poolableItem);
            }
            
            if (_showDebugLogs && _showPoolInitializerLogs)
            {
                GameObject context = gameObject;
                DateTime endDt = DateTime.Now;
                TimeSpan initializeDelayTs = endDt - startDt;
                
                UnityConsole.PrintLog("PoolManager", "InitializePool", $"{context.name} initialized in: {initializeDelayTs.TotalMilliseconds}ms.", context);
            }   
            _initialized = true;
        }

        private void Awake()
        {
            if (_dontDestroyOnLoad)
            {
                if (PlayerPrefs.HasKey(_poolManagerDataPlayerPrefsKey))
                {
                    if (_showBackgroundControlLogs) UnityConsole.PrintLog("PoolManager", "Awake", "Duplicated pool destroyed.", gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    _instanceId = GetInstanceID();
                    PlayerPrefs.SetInt(_poolManagerDataPlayerPrefsKey, _instanceId);
                    DontDestroyOnLoad(gameObject);

                    if (_showBackgroundControlLogs)
                    {
                        GameObject gm = gameObject;
                        UnityConsole.PrintLog("PoolManager", "Awake", $"{gm.name} mark as dont destroy on load.", gm);
                    }
                }
            }
            if (_poolParentTransform == null) _poolParentTransform = transform;
            if (_selfInitialize && _initializeIn == InitializeIn.Awake) InitializePool();
        }
        
        private void Start()
        {
            if (_selfInitialize && _initializeIn == InitializeIn.Start) InitializePool();
        }

        private void OnApplicationQuit()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            if (_dontDestroyOnLoad)
            {
                if(PlayerPrefs.HasKey(_poolManagerDataPlayerPrefsKey)) PlayerPrefs.DeleteKey(_poolManagerDataPlayerPrefsKey);
                if (_showBackgroundControlLogs) UnityConsole.PrintLog("PoolManager", "OnApplicationQuit", "Pool metadata deleted.", gameObject);
            }
#endif
        }

        private void OnApplicationPause(bool pauseStatus)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (_dontDestroyOnLoad)
            {
                if(PlayerPrefs.HasKey(_poolManagerDataPlayerPrefsKey)) PlayerPrefs.DeleteKey(_poolManagerDataPlayerPrefsKey);
                if (_showBackgroundControlLogs) UnityConsole.PrintLog("PoolManager", "OnApplicationPause", "Pool metadata deleted.", gameObject);
            }
#endif
        }

        /// <summary>
        /// Default pool manager constructor.
        /// </summary>
        public PoolManager()
        {
            _initialized = false;
            _showDebugLogs = true;
            _selfInitialize = true;
            _createAllObjects = false;
            _initializeIn = InitializeIn.Custom;
            _showPoolOperationLogs = true;
            _showPoolInitializerLogs = true;
        }
        
        /// <summary>
        /// Initialize game object pool.
        /// </summary>
        public void Initialize()
        {
            if (_initialized) return;
            InitializePool();
        }

        /// <summary>
        /// Add poolable item to pool.
        /// </summary>
        /// <param name="poolableItem">Poolable item.</param>
        public void AddItemToPool(PoolableItem poolableItem)
        {
            GameObject item = poolableItem.gameObject;
            poolableItem.SetParentPool(this);
            item.transform.SetParent(_poolParentTransform);
            item.SetActive(false);
            
            _pool.Enqueue(poolableItem);
            
            if (_showDebugLogs && _showPoolOperationLogs)
            {
                GameObject context = gameObject;
                UnityConsole.PrintLog("PoolManager", "AddItemToPool", $"{item.name} add to {context.name}", context);
            }
        }
        
        /// <summary>
        /// Add game object to pool.
        /// </summary>
        /// <param name="item">Game object.</param>
        public void AddObjectToPool(GameObject item)
        {
            if (item.TryGetComponent(out PoolableItem poolableItem)) AddItemToPool(poolableItem);
            else throw new ObjectsPoolException("PoolableItem component not found on instantiated game object.");
        }
        
        /// <summary>
        /// Add poolable items to pool.
        /// </summary>
        /// <param name="poolableItems">Poolable items</param>
        public void AddItemsToPool(List<PoolableItem> poolableItems)
        {
            foreach (PoolableItem poolableItem in poolableItems) AddItemToPool(poolableItem);
            if (_showDebugLogs && _showPoolOperationLogs)
            {
                GameObject context = gameObject;
                UnityConsole.PrintLog("PoolManager", "AddItemsToPool", $"{poolableItems.Count} items added to pool.", context);
            }
        }
        
        /// <summary>
        /// Add game objects to pool.
        /// </summary>
        /// <param name="items">Game objects.</param>
        public void AddObjectsToPool(List<GameObject> items)
        {
            foreach (GameObject item in items)
            {
                if (item.TryGetComponent(out PoolableItem poolableItem))
                {
                    AddItemToPool(poolableItem);
                }
                else throw new ObjectsPoolException("PoolableItem component not found on game object.");
            }
        }
        
        /// <summary>
        /// Return poolable item from pool.
        /// </summary>
        /// <returns>Game object from pool.</returns>
        public PoolableItem GetItemFromPool()
        {
            if (_pool.Count == 0 && _createOversizePrefabs)
            {
                PoolableItem newPoolableItem = PoolInitializer.CreateGameObject(PoolPrefab, _poolParentTransform, this);
                _pool.Enqueue(newPoolableItem);
                
                if (_showDebugLogs && _showPoolOperationLogs)
                {
                    GameObject context = gameObject;
                    UnityConsole.PrintLog("PoolManager", "GetItemFromPool", $"New {newPoolableItem.name} prefab instantiated and added to {name}..", context);
                }
            }

            PoolableItem poolableItemFromPool = _pool.Dequeue();
            GameObject item = poolableItemFromPool.gameObject;
            item.gameObject.SetActive(true);
            
            if (_showDebugLogs && _showPoolOperationLogs)
            {
                GameObject context = gameObject;
                UnityConsole.PrintLog("PoolManager", "GetItemFromPool", $"{item.name} returned from {context.name}", context);
            }
            return poolableItemFromPool;
        }
        
        /// <summary>
        /// Return one game object from pool.
        /// </summary>
        /// <returns>Game object from pool.</returns>
        public GameObject GetObjectFromPool()
        {
            PoolableItem poolableItem = GetItemFromPool();
            return poolableItem.gameObject;
        }
        
        /// <summary>
        /// Return poolable items from pool.
        /// </summary>
        /// <param name="count">Objects count.</param>
        /// <returns>Objects from pool.</returns>
        public List<PoolableItem> GetItemsFromPool(int count)
        {
            List<PoolableItem> items = new List<PoolableItem>();
            for (int i = 0; i < count; i++)
            {
                PoolableItem item = GetItemFromPool();
                item.gameObject.SetActive(true);
                items.Add(item);
            }
            
            if (_showDebugLogs && _showPoolOperationLogs)
            {
                GameObject context = gameObject;
                PoolableItem item = items.First();
                UnityConsole.PrintLog("PoolManager", "GetItemsFromPool", $"{count} {item.name}s returned from {context.name}", context);
            }
            return items;
        }
        
        /// <summary>
        /// Return multiple game objects from pool.
        /// </summary>
        /// <param name="count">Game objects count.</param>
        /// <returns>Game objects from pool.</returns>
        public List<GameObject> GetObjectsFromPool(int count)
        {
            List<PoolableItem> poolableItems = GetItemsFromPool(count);
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (PoolableItem poolableItem in poolableItems) gameObjects.Add(poolableItem.gameObject);
            return gameObjects;
        }
        
        /// <summary>
        /// Set custom pool prefab.
        /// </summary>
        /// <param name="prefab">Prefab to set.</param>
        public void SetPoolPrefab(GameObject prefab) => _poolPrefab = prefab;

        /// <summary>
        /// Set custom pool prefabs pattern.
        /// </summary>
        /// <param name="prefabs">Prefab list to set.</param>
        public void SetPoolPrefabs(List<GameObject> prefabs) => _prefabs = prefabs;
        
        /// <summary>
        /// Set initialize pool size objects count.
        /// </summary>
        /// <param name="poolSize">Objects count for initialize.</param>
        public void SetInitializePoolSize(int poolSize) => _initializePoolSize = poolSize;
        
        /// <summary>
        /// Set self initialize mode.
        /// </summary>
        /// <param name="mode">Pool auto initialize mode.</param>
        public void SetSelfInitializeMode(SelfInitializeMode mode) => _initializeMode = mode;

        /// <summary>
        /// Set create all prefabs value.
        /// </summary>
        /// <param name="createAllPrefabs">Do create all prefabs.</param>
        public void SetCreateAllPrefabs(bool createAllPrefabs) => _createAllObjects = createAllPrefabs;

        /// <summary>
        /// Set iterations count for self initialize.
        /// </summary>
        /// <param name="iterationsCount">Iterations count.</param>
        public void SetIterationsCount(int iterationsCount) => _iterationsCount = iterationsCount;
        
        /// <summary>
        /// Set create oversize prefabs for automatic creating game objects if pool is empty.
        /// </summary>
        /// <param name="createOversizePrefabs"></param>
        public void SetCreateOversizePrefabs(bool createOversizePrefabs) => _createOversizePrefabs = createOversizePrefabs;
    }
}
