# Pool Manager

> A component that implements the logic of a pool of game objects.

## Settings:

> All settings of the PoolManager component.

- **`Pool Parent Transform (Transform)`** - Parent object of pool objects.


- **`Create Oversize Prefabs`** - Specifies whether to create additional game objects if the pool is empty at the time of the request for the game object.


- **`Dont Destroy On Load (bool)`** - The flag responsible for marking the `DontDestroyOnLoad' object. When the scene is restarted, the duplicate object will be deleted.


- **`Pool Manager Data Player Prefs Key (string)`** - The flag responsible for marking the `DontDestroyOnLoad` object. When the scene is restarted, the duplicate object will be deleted.


- **`Self Initialize (bool)`** - The flag responsible for automatic initialization of the pool.


- **`Initialize In (InitializeIn)`** - Selecting the method in which initialization will take place. See the documentation for the initialize type.


- **`Initialize Mode (SelfInitializeMode)`** - Selecting the pool initialization mode. See the documentation for the `SelfInitializeMode` type.


- **`Pool Prefab (GameObject)`** - Prefab of the object in the pool, used for initialization.


- **`Prefabs (List<GameObject>)`** - A list of prefabs to initialize the pool.


- **`Initialize Pool Size (int)`** - The size of the pool during initialization. The number of objects to be created during initialization.


- **`Iterations Count (int)`** - Number of passes through the prefabs list.


- **`Create All Objects (bool)`** - Enabling this parameter ensures that all objects from the prefabs list will be in the pool.


- **`Show Debug Logs (bool)`** - Specifies whether to output logs for developers. `Debug.Log` is used to output logs.


- **`Show Pool Initializer Logs (bool)`** - Responsible for outputting the initialization time of the pool.


- **`Show Pool Operation Logs (bool)`** - Responsible for logging operations with checkers objects. Getting and returning a pool object.


- **`Show Background Control Logs (bool)`** - Responsible for outputting logs of the background instance monitoring module. `DontDestroyOnLoad`.


## Methods:

> All methods must be called from the created instance of the PoolManager component.

- **-** **`PoolManager.Initialize()`** **`void`** - Manual initialization of the pool. If the pool has been initialized, the method will return an error.


- **-** **`PoolManager.AddItemToPool(PoolableItem)`** **`void`** - Adds an object to the pool.


- **-** **`PoolManager.AddObjectToPool(GameObject)`** **`void`** - Adds an object to the pool.


- **-** **`PoolManager.AddItemsToPool(List<PoolableItem>)`** **`void`** - Adds a collection of objects to the pool.


- **-** **`PoolManager.AddObjectsToPool(List<GameObject>)`** **`void`** - Adds a collection of objects to the pool.


- **-** **`PoolManager.GetItemFromPool()`** **`PoolableItem`** - Returns an object from the pool.


- **-** **`PoolManager.GetObjectFromPool()`** **`GameObject`** - Returns an object from the pool.


- **-** **`PoolManager.GetItemsFromPool(int)`** **`List<PoolableItem>`** - Returns a collection of objects from the pool.


- **-** **`PoolManager.GetObjectsFromPool(int)`** **`List<GameObject>`** - Returns a collection of objects from the pool.

## Usage examples:

> Examples of using the Pool Manager component.

### Initializing the pool.

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

### Getting an object from the pool.

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

### Getting objects from the pool.

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

### Returning the object to the pool.

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

### Returning objects to the pool.

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