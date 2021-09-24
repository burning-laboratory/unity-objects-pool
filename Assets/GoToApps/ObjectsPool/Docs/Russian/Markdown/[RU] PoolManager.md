# Pool Manager

> Компонент реализующий в себе логику пула игровых объектов.

## Настройки:

- **-** **`Pool parent transform (Transform)`** - Родительский объект объектов пула.


- **-** **`Pool prefab (GameObject)`** - Префаб объекта в пуле, используется для инициализации.


- **-** **`Self initialize (bool)`** - Флаг, отвечающий за автоматическую инициализацию пула.


- **-** **`Initialize pool size (int)`** - Размер пула при инициализации. Кол-во объектов, которое нужно создать при инициализации.


- **-** **`Initialize in (InitializeIn)`** - Выбер метода в котором будет происходить инициализация. Смотрите документация к типу InitializeIn,


- **-** **`Show Debug Logs (bool)`** - Указывает нужно ли выводить логи для разработчиков. Для вывода логов используется Debug.Log.

## Методы:

> Все методы должны вызываться у созданного экземпляра компонента PoolManager.

- **-** **`PoolManager.Initialize()`** **`void`** - Ручная инициализация пула. Если пул был проинициализирован, метод выдаст ошибку.


- **-** **`PoolManager.Initialize(int)`** **`void`** - Ручная инициализация пула. Если пул был проинициализирован, метод выдаст ошибку. int - кол-во объектов, которое нужно добавить в пул при инициализации.


- **-** **`PoolManager.Initialize(int, Transform)`** **`void`** - Ручная инициализация пула. Если пул был проинициализирован, метод выдаст ошибку. int - кол-во объектов, которое нужно добавить в пул при инициализации. Transform - объект к которому будут привязываться объекты в пуле.


- **-** **`PoolManager.Initialize(GameObject, int, Transform)`** **`void`** - Ручная инициализация пула. Если пул был проинициализирован, метод выдаст ошибку. GameObject - префаб объекта пула.  int - кол-во объектов, которое нужно добавить в пул при инициализации. Transform - объект к которому будут привязываться объекты в пуле.


- **-** **`PoolManager.AddItemToPool(PoolableItem)`** **`void`** - Добавляет объект в пул.


- **-** **`PoolManager.AddObjectToPool(GameObject)`** **`void`** - Добавляет объект в пул.


- **-** **`PoolManager.AddItemsToPool(List<PoolableItem>)`** **`void`** - Добавляет коллекцию объектов в пул.


- **-** **`PoolManager.AddObjectsToPool(List<GameObject>)`** **`void`** - Добавляет коллекцию объектов в пул.


- **-** **`PoolManager.GetItemFromPool()`** **`PoolableItem`** - Возвращает объект из пула.


- **-** **`PoolManager.GetObjectFromPool()`** **`GameObject`** - Возвращает объект из пула.


- **-** **`PoolManager.GetItemsFromPool(int)`** **`List<PoolableItem>`** - Возвращает коллекцию объектов из пула.


- **-** **`PoolManager.GetObjectsFromPool(int)`** **`List<GameObject>`** - Возвращает коллекцию объектов из пула.

## Примеры использования:

> Примеры использования компонента Pool Manager.

### Инициализация пула.

> Пример кода с ручной инициализацией, при отключенной автоматической инициализации.

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

### Получение объекта из пула.

> Получение 1 объекта из пула.

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

### Получение объектов из пула.

> Получение нескольких объектов из пула.

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

### Возврат объекта в пул.

> Возврат 1 объекта в пул.

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

### Возврат объектов в пул.

> Возврат нескольких объектов в пул.

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