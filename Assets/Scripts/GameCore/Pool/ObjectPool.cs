using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCore.Pool
{
    public class ObjectPool : MonoBehaviour, IFactory<GameObject>
    {
        [SerializeField] private GameObject prefab;
        
        private List<GameObject> _objectPool = new List<GameObject>();
        private DiContainer _diContainer;

        public GameObject GetFromPool()
        {
            foreach (var objectItem in _objectPool)
            {
                if (objectItem.activeInHierarchy) continue;
                
                objectItem.SetActive(true);
                return objectItem;
            }
            
            var newObject = Create();
            newObject.SetActive(true);
            return newObject;
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject Create()
        {
            GameObject newObject = _diContainer.InstantiatePrefab(prefab);
            newObject.SetActive(false);
            _objectPool.Add(newObject);
            return newObject;
        }
    }
}