# Poolable Item

> Компонент реализующий в себе обертку объекта пула.

## Методы:

> Все методы должны вызываться у созданного экземпляра компонента Poolable Item.

- **-** **`PoolableItem.SetPool(PoolManager)`** **`void`** - Устанавливает объекту пул.


- **-** **`PoolableItem.ReturnToPool()`** **`void`** - Вовзрат объекта в пул.

## Примеры использования:

> Примеры использования компонента Poolable Item.

### Вовзрат объекта в пул игровых объектов.

> Использование компонента PoolableItem для возврата игрового объекта в пул.

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




