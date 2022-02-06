using BurningLab.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Blocks;
using BurningLab.ObjectsPool.Utils;
using UnityEngine;

namespace BurningLab.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Bullet : PoolableItem
    {
        [Header("Settings")] 
        [Tooltip("Bullet moving speed.")]
        [SerializeField] [Range(0, 10)] private float _speed;
        
        [Tooltip("Max bullet flight distance in unity units.")]
        [SerializeField] [Range(1, 50)] private float _maxBulletFlightDistance;
        
        private Transform _transform;
        private Vector3 _lastFramePosition;
        private float _flightDistance;
        
        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            Vector3 position = _transform.position;
            Vector3 targetPosition = Vector3.MoveTowards(position, position + _transform.up, _speed * Time.deltaTime);
            transform.position = targetPosition;
        }

        private void LateUpdate()
        {
            Vector3 position = _transform.position;
            if (_lastFramePosition == Vector3.zero)
            {
                _lastFramePosition = position;
                return;
            }
            
            Vector3 distanceDelta = position - _lastFramePosition;
            _flightDistance += distanceDelta.magnitude;
            if (_flightDistance >= _maxBulletFlightDistance)
            {
                _flightDistance = 0;
                UnityConsole.PrintLog("Shooting System", "Bullet", "Update", "Bullet return to pool. Bullet flew the maximum distance..");
                ReturnToPool();
                return;
            }
            
            _lastFramePosition = position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Block _))
            {
                _flightDistance = 0;
                ReturnToPool();
            }
        }
    }
}