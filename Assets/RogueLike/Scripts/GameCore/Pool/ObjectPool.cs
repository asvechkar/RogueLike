using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace RogueLike.Scripts.GameCore.Pool
{
    public class ObjectPool : MonoBehaviour, IFactory<GameObject>
    {
        [SerializeField] private GameObject prefab;
        
        private List<GameObject> _objectPool = new();
        private DiContainer _diContainer;

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

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject Create()
        {
            var newObject = _diContainer.InstantiatePrefab(prefab);
            newObject.SetActive(false);
            _objectPool.Add(newObject);
            return newObject;
        }
    }
}