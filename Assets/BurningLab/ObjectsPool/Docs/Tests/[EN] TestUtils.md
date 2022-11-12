# Test Utils

> A class with utilities to simplify writing texts.

## Methods:

> All methods are static and no instantiation of the TestUtils class is required to call them.

- **-** **`TestUtils.CreatePoolManagerInstance()`** **`PoolManager`** - Creates a game object on the stage with a `PoolManager` with default settings.


- **-** **`TestUtils.CreateGameObjectExtendedFromPoolableItem()`** **`GameObject`** - Creates a game object on the stage with a text component inherited from `PoolableItem`.


- **-** **`TestUtils.CreateGameObjectExtendedFromPoolableItem(string name)`** **`GameObject`** - Creates a game object on the stage with a text component inherited from `PoolableItem`. The name of the game object is passed as the `name` parameter.


- **-** **`TestUtils.CreatePoolableItemExtendedFromPoolableItem()`** **`PoolableItem`** - Creates a game object on the stage with a text component inherited from `PoolableItem`.


- **-** **`TestUtils.GenerateRandomTemplate()`** **`List<GameObject>`** - Generates and returns a random sequence of prefabs.