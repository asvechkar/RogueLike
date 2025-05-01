using UnityEngine;
using UnityEngine.Pool;

namespace RogueLike.Scripts.GameCore.Pool
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject poolablePrefab;
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;
        
        private IObjectPool<GameObject> _pool;

        public GameObject GetFromPool()
        {
            return _pool.Get();
        }

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: CreatePoolableObject,
                actionOnGet: OnGetFromPool,
                actionOnRelease:  OnReleaseFromPool,
                actionOnDestroy: OnDestroyPoolObject,
                collectionCheck: collectionCheck,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
                );
        }

        private GameObject CreatePoolableObject()
        {
            var poolableObject = Instantiate(poolablePrefab);
            
            poolableObject.GetComponent<Poolable>().Pool = _pool;
            poolableObject.SetActive(false);
            return poolableObject;
        }

        private void OnGetFromPool(GameObject poolableObject)
        {
            poolableObject.SetActive(false);
        }

        private void OnReleaseFromPool(GameObject poolableObject)
        {
            poolableObject.SetActive(false);
        }

        private void OnDestroyPoolObject(GameObject poolableObject)
        {
            Destroy(poolableObject);
        }
    }
}