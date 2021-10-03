using System;
using System.Collections.Generic;
using GoToApps.ObjectsPool.Exceptions;
using UnityEngine;
using GoToApps.ObjectsPool.Types;
using GoToApps.ObjectsPool.Utils;
using Random = System.Random;

namespace GoToApps.ObjectsPool
{
    /// <summary>
    /// Objects pool manager.
    /// </summary>
    [AddComponentMenu("GoTo-Apps/Objects Pool/Pool Manager")]
    public class PoolManager : MonoBehaviour
    {
        [Tooltip("Parent game object transform for game objects in pool.")]
        [SerializeField] private Transform _poolParentTransform;

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
        
        private bool _initialized;
        private int _instanceId;
        
        /// <summary>
        /// Mix array.
        /// </summary>
        /// <param name="data">Array to mix.</param>
        /// <typeparam name="T">Array items type.</typeparam>
        /// <returns>Mixed array.</returns>
        private List<T> MixArray<T>(List<T> data)
        {
            Random random = new Random();
            for (int i = data.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (data[j], data[i]) = (data[i], data[j]);
            }

            return data;
        }

        /// <summary>
        /// Create poolable item and link to this pool.
        /// </summary>
        /// <returns>Created poolable item</returns>
        private PoolableItem CreatePoolableItem(GameObject prefab, Transform parent)
        {
            GameObject initializedItem = Instantiate(prefab, parent);
            if (initializedItem.TryGetComponent(out PoolableItem poolableItem))
            {
                poolableItem.SetPool(this);
                initializedItem.SetActive(false);
                return poolableItem;
            }
            throw new ObjectsPoolException("PoolableItem component not found on instantiated game object.");
        }

        /// <summary>
        /// Initialize pool.
        /// </summary>
        private void InitializePool()
        {
            DateTime startDt = DateTime.Now;
            switch (_initializeMode)
            {
                case SelfInitializeMode.SinglePrefab:
                    for (int i = 0; i < _initializePoolSize; i++)
                    {
                        PoolableItem poolableItem = CreatePoolableItem(_poolPrefab, _poolParentTransform);
                        _pool.Enqueue(poolableItem);
                    }
                    break;
                
                case SelfInitializeMode.MultiplePrefabs:
                    List<PoolableItem> poolableItems = new List<PoolableItem>();
                    if (_createAllObjects)
                    {
                        for (int i = 0; i < _iterationsCount; i++)
                        {
                            foreach (GameObject prefab in _prefabs)
                            {
                                PoolableItem poolableItem = CreatePoolableItem(prefab, _poolParentTransform);
                                poolableItems.Add(poolableItem);
                            }
                        }

                        poolableItems = MixArray(poolableItems);
                        foreach (PoolableItem poolableItem in poolableItems)
                        {
                            _pool.Enqueue(poolableItem);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _initializePoolSize; i++)
                        {
                            int index = UnityEngine.Random.Range(0, _prefabs.Count);
                            GameObject prefab = _prefabs[index];
                            PoolableItem poolableItem = CreatePoolableItem(prefab, _poolParentTransform);
                            _pool.Enqueue(poolableItem);
                        }
                    }
                    break;
                
                case SelfInitializeMode.InitializeTemplate:
                    for (int i = 0; i < _iterationsCount; i++)
                    {
                        foreach (GameObject prefab in _prefabs)
                        {
                            PoolableItem poolableItem = CreatePoolableItem(prefab, _poolParentTransform);
                            _pool.Enqueue(poolableItem);
                        }
                    }
                    break;
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
            if (item.TryGetComponent(out PoolableItem poolableItem))
            {
                AddItemToPool(poolableItem);
            }
            else throw new ObjectsPoolException("PoolableItem component not found on instantiated game object.");
        }
        
        /// <summary>
        /// Add poolable items to pool.
        /// </summary>
        /// <param name="poolableItems">Poolable items</param>
        public void AddItemsToPool(List<PoolableItem> poolableItems)
        {
            foreach (PoolableItem poolableItem in poolableItems)
            {
                GameObject item = poolableItem.gameObject;
                item.transform.SetParent(_poolParentTransform);
                item.SetActive(false);
                _pool.Enqueue(poolableItem);
            }

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
            if (_pool.Count == 0)
            {
                // TODO: Add auto oversize.
                throw new ObjectsPoolException("There are not enough objects in the pool.");
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
            if (_pool.Count < count)
            {
                // TODO: Add auto oversize.
                throw new ObjectsPoolException("There are not enough objects in the pool.");
            }
            
            List<PoolableItem> items = new List<PoolableItem>();
            for (int i = 0; i < count; i++)
            {
                PoolableItem item = _pool.Dequeue();
                item.gameObject.SetActive(true);
                items.Add(item);
            }
            
            if (_showDebugLogs && _showPoolOperationLogs)
            {
                GameObject context = gameObject;
                UnityConsole.PrintLog("PoolManager", "GetItemsFromPool", $"{count} {_poolPrefab.name} returned from {context.name}", context);
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
    }
}
