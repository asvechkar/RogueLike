using RogueLike.Scripts.Weapon;
using UnityEngine;
using UnityEngine.Pool;

namespace RogueLike.Scripts.GameCore.Pool
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;
        
        private IObjectPool<GameObject> _pool;

        public GameObject GetProjectile()
        {
            return _pool.Get();
        }

        public T GetProjectile<T>() where T : Component
        {
            var poolItem = _pool.Get();
            return poolItem.GetComponent<T>();
        }

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: CreateProjectile,
                actionOnGet: OnGetFromPool,
                actionOnRelease:  OnReleaseFromPool,
                actionOnDestroy: OnDestroyPoolObject,
                collectionCheck: collectionCheck,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
                );
        }

        private GameObject CreateProjectile()
        {
            var projectileInstance = Instantiate(projectilePrefab);
            projectileInstance.GetComponent<Projectile>().Pool = _pool;
            projectileInstance.SetActive(false);
            return projectileInstance;
        }

        private void OnGetFromPool(GameObject projectile)
        {
            projectile.SetActive(false);
        }

        private void OnReleaseFromPool(GameObject projectile)
        {
            projectile.SetActive(false);
        }

        private void OnDestroyPoolObject(GameObject projectile)
        {
            Destroy(projectile);
        }
    }
}