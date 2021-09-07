using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoToApps.ObjectsPool.Examples.Single_Pool_Example.Scripts.SinglePoolDemo
{
    /// <summary>
    /// Prefabs spawner.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private PoolManager _pool;
        [SerializeField] private Transform _spawnedBlocksParent;
        
        [SerializeField] [Range(0, 10)] private float _spawnDelay;
        [SerializeField] [Range(0, 3)] private float _horizontalSpawnRange;

        private void Start()
        {
            StartCoroutine(SpawnPrefabs());
        }

        private IEnumerator SpawnPrefabs()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                
                GameObject prefab = _pool.GetObjectFromPool();
                prefab.transform.SetParent(_spawnedBlocksParent);
                float xOffset = Random.Range(-_horizontalSpawnRange, _horizontalSpawnRange);
                Vector3 localPosition = transform.localPosition;
                Vector3 prefabLocalPosition = new Vector3 {
                    x = localPosition.x + xOffset,
                    y = localPosition.y,
                    z = localPosition.z
                };
                prefab.transform.localPosition = prefabLocalPosition;
            }
        }
    }
}