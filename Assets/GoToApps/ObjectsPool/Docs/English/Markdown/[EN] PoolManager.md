# Pool Manager

> A component that implements the logic of a pool of game objects.

## Settings:

- **-** **`Pool parent transform (Transform)`** - The parent object transform of the pool objects.


- **-** **`Pool prefab (GameObject)`** - The prefab of the object in the pool, used for initialization.


- **-** **`Self initialize (bool)`** - The flag responsible for automatic initialization of the pool.


- **-** **`Initialize pool size (int)`** - The size of the pool during initialization. The number of objects to be created during initialization.


- **-** **`Initialize int (InitializeIn)`** - Select the method in which initialization will take place. See the documentation for the InitializeIn type,


- **-** **`Show Debug Logs (bool)`** - Specifies whether to output logs for developers. To output logs, Debug.Log is used.

## Methods:

> All methods must be called from the created instance of the PoolManager component.

- **-** **`PoolManager.Initialize()`** **`void`** - Manual initialization of the pool. If the pool was initialized, the method will return an error.


- **-** **`PoolManager.Initialize(int)`** **`void`** - Manual initialization of the pool. If the pool was initialized the method will return an error. int - the number of objects to be added to the pool during initialization.


- **-** **`PoolManager.Initialize(int, Transform)`** **`void`** - Manual initialization of the pool. If the pool was initialized the method will return an error. int - the number of objects to be added to the pool during initialization. Transform-the object to which the objects in the pool will be bound.


- **-** **`PoolManager.Initialize(GameObject, int, Transform)`** **`void`** - Manual initialization of the pool. If the pool was initialized, the method will return an error. GameObject-the prefab of the pool object. int - the number of objects to be added to the pool during initialization. Transform-the object to which the objects in the pool will be bound.


- **-** **`PoolManager.AddItemToPool(PoolableItem)`** **`void`** - Add 'PoolableItem' to pool.


- **-** **`PoolManager.AddObjectToPool(GameObject)`** **`void`** - Add object to pool..


- **-** **`PoolManager.AddItemsToPool(List<PoolableItem>)`** **`void`** - Add poolable items collection to pool..


- **-** **`PoolManager.AddObjectsToPool(List<GameObject>)`** **`void`** - Add game objects collection to pool..


- **-** **`PoolManager.GetItemFromPool()`** **`PoolableItem`** - Return PoolableItem from pool.


- **-** **`PoolManager.GetObjectFromPool()`** **`GameObject`** - Return game object from pool.


- **-** **`PoolManager.GetItemsFromPool(int)`** **`List<PoolableItem>`** - Return list of poolable items from pool..


- **-** **`PoolManager.GetObjectsFromPool(int)`** **`List<GameObject>`** - Return list of game objects from pool..

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