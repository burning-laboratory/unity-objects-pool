using UnityEngine;

namespace GoToApps.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Blocks
{
    public class BlockMover : MonoBehaviour
    {
        [Header("Settings")] 
        [Tooltip("Block moving speed.")]
        [SerializeField] [Range(0, 10)] private float _speed;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            Vector3 position = _transform.position;
            Vector3 targetPosition = Vector3.MoveTowards(position, position + Vector3.down, _speed * Time.deltaTime);
            transform.position = targetPosition;
        }
    }
}