using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GoToApps.ObjectsPool.Examples.Multiple_Pools_Example.Scripts.MultiplePoolDemo.Gameplay.Blocks
{
    public class PoolInitializer : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private PoolManager _blocksPool;

        [Header("Settings")] 
        [Tooltip("Prefabs list.")]
        [SerializeField] private List<PoolableItem> _blockPrefabs;
        
        [Tooltip("Spawn block iterations count.")]
        [SerializeField] private int _iterationsCount;
        
        /// <summary>
        /// Return array copy.
        /// </summary>
        /// <param name="list">List for copy.</param>
        /// <typeparam name="T">List type.</typeparam>
        /// <returns>List copy.</returns>
        private List<T> CopyList<T>(List<T> list)
        {
            T[] tempArray = new T[list.Count];
            list.CopyTo(tempArray);
            return tempArray.ToList();
        }

        private void Awake()
        {
            List<PoolableItem> mixedPrefabs = new List<PoolableItem>();

            for (int i = 0; i < _iterationsCount; i++)
            {
                List<PoolableItem> blockPrefabs = CopyList(_blockPrefabs);
                while (blockPrefabs.Count != 0)
                {
                    int index = Random.Range(0, blockPrefabs.Count);
                    PoolableItem prefab = blockPrefabs[index];
                    mixedPrefabs.Add(prefab);
                    
                    blockPrefabs.Remove(prefab);
                    List<PoolableItem> blockPrefabsForNextIteration = new List<PoolableItem>();
                    foreach (PoolableItem item in blockPrefabs)
                    {
                        if (item == null) continue;
                        blockPrefabsForNextIteration.Add(item);
                    }

                    blockPrefabs = blockPrefabsForNextIteration;
                }
            }

            foreach (PoolableItem prefab in mixedPrefabs)
            {
                GameObject prefabGm = Instantiate(prefab.gameObject, _blocksPool.transform);
                PoolableItem poolableItem = prefabGm.GetComponent<PoolableItem>();
                poolableItem.SetPool(_blocksPool);
                _blocksPool.AddObjectToPool(prefabGm);
            }
        }
    }
}