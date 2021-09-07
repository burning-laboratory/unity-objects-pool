using UnityEngine;

namespace GoToApps.ObjectsPool.Examples.Single_Pool_Example.Scripts.SinglePoolDemo
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class SomePoolableItem : PoolableItem
    {
        [Header("Settings")] 
        [SerializeField] [Range(0, 1.5f)] private float _speed;
        
        private void Update()
        {
            Vector3 position = transform.position;
            Vector3 direction = Vector3.down;
            
            Vector3 targetPosition = Vector3.Lerp(position, position + direction, _speed * Time.deltaTime);
            
            transform.position = targetPosition;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Destroyer destroyer))
            {
                ReturnToPool();   
            }
        }
    }
}