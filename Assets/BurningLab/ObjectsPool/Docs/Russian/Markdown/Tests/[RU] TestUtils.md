# Test Utils

> Класс с утилитами для упрощения написания тестов.

## Методы:

> Все методы являются статичными и для их вызова не требуется создание экземпляра класса TestUtils.

- **-** **`TestUtils.CreatePoolManagerInstance()`** **`PoolManager`** - Создает на сцене игровой объект с менеджером пула с настройками по умолчанию.


- **-** **`TestUtils.CreateGameObjectExtendedFromPoolableItem()`** **`GameObject`** - Создает на сцене игровой объект с тестовым компонентом унаследованным от PoolableItem.


- **-** **`TestUtils.CreateGameObjectExtendedFromPoolableItem(string name)`** **`GameObject`** - Создает на сцене игровой объект с тестовым компонентом унаследованным от PoolableItem. В качестве параметра `name` передается имя игрового объекта.


- **-** **`TestUtils.CreatePoolableItemExtendedFromPoolableItem()`** **`PoolableItem`** - Создает на сцене игровой объект с тестовым компонентом унаследованным от PoolableItem.


- **-** **`TestUtils.GenerateRandomTemplate()`** **`List<GameObject>`** - Генерирует и возвращает случайную последовательность префабов.