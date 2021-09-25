# Pool Manager

> Компонент реализующий в себе логику пула игровых объектов.

## Настройки:

> Все настройки компонента PoolManager.

- **`Pool parent transform (Transform)`** - Родительский объект объектов пула.


- **`Self initialize (bool)`** - Флаг, отвечающий за автоматическую инициализацию пула.


- **`Initialize in (InitializeIn)`** - Выбер метода в котором будет происходить инициализация. Смотрите документация к типу InitializeIn.


- **`Initialize Mode (SelfInitializeMode)`** - Выбор режима инициализации пула. Смотрите документацию к типу `SelfInitializeMode`.


- **`Pool prefab (GameObject)`** - Префаб объекта в пуле, используется для инициализации.


- **`Prefabs (List<GameObject>)`** - Список префабов для инициализации пула.


- **`Initialize pool size (int)`** - Размер пула при инициализации. Кол-во объектов, которое нужно создать при инициализации.


- **`Iterations Count (int)`** - Кол-во проходов по списку префабов.


- **`Create All Objects (bool)`** - Включение этого параметра гарантирует что в пуле будут все объекты из списка префабов.


- **`Show Debug Logs (bool)`** - Указывает нужно ли выводить логи для разработчиков. Для вывода логов используется Debug.Log.


- **`Show Pool Initializer Logs (bool)`** - Отвечает за вывод времени инициализации пула.


- **`Show Pool Operation Logs (bool)`** - Отвечает за логирование операций с объектами пула. Получение и возврат объектов пула.


## Методы:

> Все методы должны вызываться у созданного экземпляра компонента PoolManager.

- **-** **`PoolManager.Initialize()`** **`void`** - Ручная инициализация пула. Если пул был проинициализирован, метод выдаст ошибку.


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