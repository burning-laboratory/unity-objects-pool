# Self Initialize Type

> An enumeration used to specify the type of automatic initialization of a pool of game objects.

## Values:

- **-** **`SelfInitializeType.SinglePrefab`** - Initialization mode with a single prefab.
  In this mode, the number of created prefab objects specified in the settings will be added to the pool during initialization.


- **-** **`SelfInitializeType.MultiplePrefabs`** - Multiple prefab initialization mode. 
  In this mode, objects from the list will be added to the pool in random order.
  If you need all the items from the list to be present in the pool, enable the `Create all objects` parameter.
  When the `Create all objects` parameter is enabled, you can specify the number of passes through the list in the `Iteration count` parameter.
  When creating objects in the pool, they will be shuffled.


- **-** **`SelfInitializeType.Custom`** - Template initialization mode.
  In this mode, during initialization, all objects from the list will be created in the same order in which they are specified.
  You can specify the number of passes through the list during initialization in the `Iterations count` parameter.





