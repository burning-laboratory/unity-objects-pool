using BurningLab.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Shooting;
using UnityEngine;

namespace BurningLab.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Blocks
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Block : PoolableItem
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out BlocksDestroyer _) || other.gameObject.TryGetComponent(out Bullet _))
            {
                ReturnToPool();
            }
        }
    }
}