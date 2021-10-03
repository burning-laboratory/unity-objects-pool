# Pool Manager

> A component that implements the logic of a pool of game objects.

## Settings:

> All settings of the PoolManager component.

- **`Pool Parent Transform (Transform)`** - The parent object of the pool objects.


- **`Dont Destroy On Load (bool)`** - The flag responsible for marking the `DontDestroyOnLoad` object. When the scene is restarted, the duplicate object will be deleted.


- **`Pool Manager Data Player Prefs Key (string)`** - The key by which the pool manager will save data. We recommend using unique keys for each instance of the pool manager.


- **`Self Initialize (bool)`** - The flag responsible for automatic initialization of the pool.


- **`Initialize In (InitializeIn)`** - Selecting the method in which initialization will take place. See the documentation for the `InitializeIn` type.


- **`Initialize Mode (SelfInitializeMode)`** - Selecting the pool initialization mode. See the documentation for the `SelfInitializeMode` type.


- **`Pool Prefab (GameObject)`** - The prefab of the object in the pool, used for initialization.


- **`Prefabs (List<GameObject>)`** - A list of prefabs for initializing the pool.


- **`Initialize Pool Size (int)`** - The size of the pool during initialization. The number of objects to be created during initialization.


- **`Iterations Count (int)`** - Number of passes through the list of prefabs.


- **`Create All Objects (bool)`** - Enabling this parameter ensures that all objects from the list of prefabs will be in the pool.


- **`Show Debug Logs (bool)`** - Specifies whether to output logs for developers. To output logs, `Debug.Log` is used.


- **`Show Pool Initializer Logs (bool)`** - Responsible for displaying the initialization time of the pool.


- **`Show Pool Operation Logs (bool)`** - Responsible for logging operations with checkers objects. A streamlined pool was received and called.


- **`Show Background Control Logs (bool)`** - Responsible for outputting logs of the background instance monitoring module. `ContDestroyOnLoad`.


## Methods:

> All methods must be called from the created instance of the PoolManager component.

- **-** **`PoolManager.Initialize()`** **`void`** - Manual initialization of the pool. If the pool was initialized, the method will return an error.


- **-** **`PoolManager.AddItemToPool(PoolableItem)`** **`void`** - Add 'PoolableItem' to pool.


- **-** **`PoolManager.AddObjectToPool(GameObject)`** **`void`** - Add object to pool.


- **-** **`PoolManager.AddItemsToPool(List<PoolableItem>)`** **`void`** - Add poolable items collection to pool.


- **-** **`PoolManager.AddObjectsToPool(List<GameObject>)`** **`void`** - Add game objects collection to pool.


- **-** **`PoolManager.GetItemFromPool()`** **`PoolableItem`** - Return PoolableItem from pool.


- **-** **`PoolManager.GetObjectFromPool()`** **`GameObject`** - Return game object from pool.


- **-** **`PoolManager.GetItemsFromPool(int)`** **`List<PoolableItem>`** - Return list of poolable items from pool.


- **-** **`PoolManager.GetObjectsFromPool(int)`** **`List<GameObject>`** - Return list of game objects from pool.

## Examples:

> Pool Manager usage examples.

### Initialize pool.

> Code example with manual initialization, with automatic initialization disabled.

```c#
    public class PoolInitializer : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private PoolManager _pool;

        [Header("Settings")] 
        [SerializeField] private GameObject _poolPrefab;
        [SerializeField] private int _initializeCount;
        [SerializeField] private Transform _poolParentTransform;
        
        private void Awake()
        {
            _pool.Initialize(_poolPrefab, _initializeCount, _poolParentTransform);
        }
    }
```

### Return object from pool.

> Getting 1 object from the pool.

```c#
    public class ExampleComponent : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private PoolManager _bulletsPool;

        public void SomeMethod()
        {
            GameObject bullet = _bulletsPool.GetItemFromPool();
        }
    }
```

### Return objects from pool.

> Getting multiple objects from a pool.

```c#
    public class ExampleComponent : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private PoolManager _bulletsPool;

        public void SomeMethod()
        {
            int bulletsCount = 100;
            List<GameObject> bullets = _bulletsPool.GetItemsFromPool(bulletsCount);
        }
    }
```

### Add game object to pool.

> Return 1 object to the pool.

```c#
    public class ExampleComponent : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private PoolManager _bulletsPool;

        public void SomeMethod()
        {
            GameObject bullet = new GameObject();
            
            _bulletsPool.AddItemToPool(bullet);
        }
    }
```

### Add game objects ro pool.

> Returning several objects to the pool.

```c#
    public class ExampleComponent : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private PoolManager _bulletsPool;

        public void SomeMethod()
        {
            List<GameObject> bullets = new List<GameObject>();
            
            _bulletsPool.AddItemsToPool(bullets);
        }
    }
```