using UnityEngine;

namespace GoToApps.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Shooting
{
    public class Cannon : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PoolManager _bulletsPool;
        [SerializeField] private Transform _bulletSpawnerTransform;
        [SerializeField] private Transform _bulletsContainerTransform;

        private Transform _transform;
        
        /// <summary>
        /// Shoot button down event handler.
        /// </summary>
        private void OnShootButtonDown()
        {
            GameObject bulletGm = _bulletsPool.GetObjectFromPool();
            bulletGm.transform.SetParent(_bulletsContainerTransform);
            bulletGm.transform.up = _transform.up;
            bulletGm.transform.position = _bulletSpawnerTransform.position;
        }
        
        private void Awake()
        {
            _transform = transform;
            if (_mainCamera == null) _mainCamera = Camera.main;
        }
        
        private void Update()
        {
            Transform cachedTransform = transform;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition); 
            float angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);
            Vector3 eulerAngles = new Vector3(0f, 0f, cachedTransform.position.y < mousePosition.y ? angle : -angle);
            eulerAngles.z -= 90;
            transform.eulerAngles = eulerAngles;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0)) OnShootButtonDown();
        }
    }
}