using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningLab.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Blocks
{
    public class BlocksSpawner : MonoBehaviour
    {
        [Header("Components")] 
        [Tooltip("Blocks pool.")]
        [SerializeField] private PoolManager _blocksPool;
        
        [Tooltip("Blocks parent game object.")]
        [SerializeField] private Transform _blocksParentTransform;

        [Header("Settings")] 
        [Tooltip("Blocks spawn waves delay.")]
        [SerializeField] private float _spawnDelay;
        
        [Tooltip("Block spawn points list.")]
        [SerializeField] private List<Transform> _spawnPoints;
        
        /// <summary>
        /// Block spawner coroutine.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Spawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                
                int index = Random.Range(0, _spawnPoints.Count);
                Vector3 blockPosition = _spawnPoints[index].position;
                
                GameObject blockGm = _blocksPool.GetObjectFromPool();
                blockGm.transform.SetParent(_blocksParentTransform);
                blockGm.transform.position = blockPosition;
            }
        }

        private void Start()
        {
            StartCoroutine(Spawner());
        }
    }
}