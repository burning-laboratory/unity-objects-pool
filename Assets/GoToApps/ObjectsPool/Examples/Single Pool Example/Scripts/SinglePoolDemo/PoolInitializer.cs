using System.Collections.Generic;
using GoToApps.ObjectsPool.Utils;
using UnityEngine;

namespace GoToApps.ObjectsPool.Examples.Single_Pool_Example.Scripts.SinglePoolDemo
{
    public class PoolInitializer : MonoBehaviour
    {
        [Header("Pool")] 
        [SerializeField] private PoolManager _pool;
        
        [Header("Settings")] 
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private int _objectsCountForInitialize;
        
        private void Awake()
        {
            for (int i = 0; i < _objectsCountForInitialize; i++)
            {
                int index = Random.Range(0, _prefabs.Count);
                GameObject prefab = _prefabs[index];

                GameObject instantiatedGm = Instantiate(prefab, _pool.transform);
                if (instantiatedGm.TryGetComponent(out PoolableItem poolableItem))
                {
                    poolableItem.SetPool(_pool);
                    poolableItem.ReturnToPool();
                }
            }
            
            UnityConsole.PrintLog("Single Pool Example App", "PoolInitializer", "Awake", "Objects instantiated and add to pool.", gameObject);
        }
    }
}