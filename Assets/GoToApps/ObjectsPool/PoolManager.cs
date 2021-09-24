using System.Collections.Generic;
using GoToApps.ObjectsPool.Exceptions;
using UnityEngine;
using GoToApps.ObjectsPool.Types;
using GoToApps.ObjectsPool.Utils;

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

        [Tooltip("Automatic pool initialize.")]
        [SerializeField] private bool _selfInitialize;
        
        [Tooltip("Set initialize pool method.")]
        [SerializeField] private InitializeIn _initializeIn;
        
        [Tooltip("Type of self pool initialization.")]
        //TODO: Add property to pool manager documentation.
        [SerializeField] private SelfInitializeType _initializeType;

        [Tooltip("The number of passes through the list during initialization.")]
        //TODO: Add property to pool manager documentation.
        [SerializeField] private int _iterationsCount;

        [Tooltip("The need to create each object from the list. Enabling this parameter ensures that after initialization, all the objects from the list above will be in the pool.")]
        //TODO: Add property to pool manager documentation.
        [SerializeField] private bool _createAllObjects;

        [Tooltip("Prefabs list.")]
        //TODO: Add property to pool manager documentation.
        [SerializeField] private List<GameObject> _prefabs;

        [Tooltip("Game object prefab.")]
        [SerializeField] private GameObject _poolPrefab;

        [Tooltip("Set initialize game objects count.")]
        [SerializeField] private int _initializePoolSize;

        [Tooltip("Show debug logs.")]
        [SerializeField] private bool _showDebugLogs;
        
        private readonly Queue<PoolableItem> _pool = new Queue<PoolableItem>();
        
        private bool _initialized;
        
        /// <summary>
        /// Create poolable item and link to this pool.
        /// </summary>
        /// <returns>Created poolable item</returns>
        private PoolableItem CreatePoolableItem()
        {
            GameObject initializedItem = Instantiate(_poolPrefab, _poolParentTransform);
            if (initializedItem.TryGetComponent(out PoolableItem poolableItem))
            {
                poolableItem.SetPool(this);
                initializedItem.SetActive(false);
                return poolableItem;
            }
            else throw new ObjectsPoolException("PoolableItem component not found on instantiated game object.");
        }

        /// <summary>
        /// Initialize pool.
        /// </summary>
        private void InitializePool()
        {
            for (int i = 0; i < _initializePoolSize; i++)
            {
                PoolableItem poolableItem = CreatePoolableItem();
                _pool.Enqueue(poolableItem);
            }
            
            if (_showDebugLogs)
            {
                GameObject context = gameObject;
                UnityConsole.PrintLog("PoolManager", "InitializePool", $"Instantiated {_initializePoolSize} {_poolPrefab.name}", context);
            }   
            _initialized = true;
        }

        private void Awake()
        {
            if (_poolParentTransform == null) _poolParentTransform = transform;
            
            if (_selfInitialize && _initializeIn == InitializeIn.Awake)
            {
                InitializePool();
                if (_showDebugLogs)
                {
                    GameObject context = gameObject;
                    UnityConsole.PrintLog("PoolManager", "Awake", $"{context.name} successful initialized.", context);
                }
            }
        }
        
        private void Start()
        {
            if (_selfInitialize && _initializeIn == InitializeIn.Start)
            {
                InitializePool();
                if (_showDebugLogs)
                {
                    GameObject context = gameObject;
                    UnityConsole.PrintLog("PoolManager", "Awake", $"{context.name} successful initialized.", context);
                }
            }
        }
        
        /// <summary>
        /// Default pool manager constructor.
        /// </summary>
        public PoolManager()
        {
            _initialized = false;
            _showDebugLogs = true;
            _selfInitialize = true;
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
        /// Initialize game objects pool.
        /// </summary>
        /// <param name="count">Initialize game objects count.</param>
        public void Initialize(int count)
        {
            if (_initialized) return;
            _initializePoolSize = count;
            InitializePool();
        }
        
        /// <summary>
        /// Initialize game objects pool.
        /// </summary>
        /// <param name="count">Initialize game objects count.</param>
        /// <param name="transform">Game objects parent transform.</param>
        public void Initialize(int count, Transform transform)
        {
            if (_initialized) return;
            _initializePoolSize = count;
            _poolParentTransform = transform;
            InitializePool();
        }
        
        /// <summary>
        /// Initialize game objects pool.
        /// </summary>
        /// <param name="prefab">Game object prefab.</param>
        /// <param name="count">Initialize game objects count.</param>
        /// <param name="transform">Game objects parent transform.</param>
        public void Initialize(GameObject prefab, int count, Transform transform)
        {
            if (_initialized) return;
            _initializePoolSize = count;
            _poolParentTransform = transform;
            _poolPrefab = prefab;
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
            
            if (_showDebugLogs)
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

            if (_showDebugLogs)
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
                else throw new ObjectsPoolException("PoolableItem component not found on instantiated game object.");
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
                PoolableItem poolableItem = CreatePoolableItem();
                _pool.Enqueue(poolableItem);
                
                if (_showDebugLogs)
                {
                    GameObject context = gameObject;
                    UnityConsole.PrintLog("PoolManager", "InitializePool", $"Instantiated {poolableItem.name}", context);
                }
            }
            
            PoolableItem poolableItemFromPool = _pool.Dequeue();
            GameObject item = poolableItemFromPool.gameObject;
            item.gameObject.SetActive(true);
            
            if (_showDebugLogs)
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
                int countDelta = count - _pool.Count;
                for (int i = 0; i < countDelta; i++)
                {
                    PoolableItem poolableItem = CreatePoolableItem();
                    _pool.Enqueue(poolableItem);
                }

                if (_showDebugLogs)
                {
                    GameObject context = gameObject;
                    UnityConsole.PrintLog("PoolManager", "GetItemsFromPool", $"Initialized {countDelta}  {_poolPrefab.name}", context);
                }
            }
            
            List<PoolableItem> items = new List<PoolableItem>();
            for (int i = 0; i < count; i++)
            {
                PoolableItem item = _pool.Dequeue();
                item.gameObject.SetActive(true);
                items.Add(item);
            }
            
            if (_showDebugLogs)
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
