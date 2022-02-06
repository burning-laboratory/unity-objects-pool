# Poolable Item

> A component that implements the wrapper of the pool object.

## Properties:

- **-** **`PoolableItem.ParentPool`** **`PoolManager`** - A reference to the pool to which the object is linked.

## Methods:

> All methods must be called from the created instance of the Poolable Item component.

- **-** **`PoolableItem.SetParentPool(PoolManager)`** **`void`** - Sets a pool for the object.


- **-** **`PoolableItem.ReturnToPool()`** **`void`** - Returning the object to the pool.

## Usage examples:

> Examples of using the Poolable Item component.

### Returning an object to the pool of game objects.

> Using the PoolableItem component to return the game object to the pool.

```c#
    public class SomePoolableItem : PoolableItem
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Destroyer destroyer))
            {
                ReturnToPool();   
            }
        }
    }
```




