using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour, IActivate
    {
        [SerializeField] private float timeToSpawn;
        [SerializeField] private ObjectPool enemyPool;
        
        private WaitForSeconds _interval;
        private Coroutine _spawnCoroutine;

        private void Start()
        {
            _interval = new WaitForSeconds(timeToSpawn);
        }
        
        private IEnumerator Spawn()
        {
            while (true)
            {
                var enemy = enemyPool.GetFromPool();
                enemy.transform.SetParent(transform);
                enemy.transform.position = transform.position;

                yield return _interval;
            }
        }

        public void Activate()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public void Deactivate()
        {
            if (_spawnCoroutine == null) return;
            
            StopCoroutine(_spawnCoroutine);
        }
    }
}