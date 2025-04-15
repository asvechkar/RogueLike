using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        
        private List<GameObject> _objectPool = new();

        public GameObject GetFromPool()
        {
            foreach (var poolItem in _objectPool.Where(poolItem => !poolItem.activeInHierarchy))
            {
                poolItem.SetActive(true);
                return poolItem;
            }
            
            var newObject = Create();
            newObject.SetActive(true);
            return newObject;
        }
        
        public GameObject Create()
        {
            var newObject = Instantiate(prefab);
            newObject.SetActive(false);
            _objectPool.Add(newObject);
            return newObject;
        }
    }
}