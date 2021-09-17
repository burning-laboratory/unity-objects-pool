# Poolable Item

> A component that implements the wrapper of the pool object.

## Methods:

> All methods must be called from the created instance of the Poolable Item component.

- **-** **`PoolableItem.SetPool(PoolManager)`** **`void`** - Sets the pool to which this object belongs to the object.


- **-** **`PoolableItem.ReturnToPool()`** **`void`** - The object's return to the pool.

## Examples:

> Examples of using the Poolable Item component.

### The object's return to the pool of game objects.

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




