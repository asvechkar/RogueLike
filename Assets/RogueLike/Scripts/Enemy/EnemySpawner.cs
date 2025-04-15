using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Pool;
using RogueLike.Scripts.Player;
using UnityEngine;

namespace RogueLike.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour, IActivate
    {
        [SerializeField] private float timeToSpawn;
        [SerializeField] private Transform minPos, maxPos;
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private ObjectPool enemyPool;

        [Inject] private PlayerMovement _playerMovement;
        [Inject] private GetRandomSpawnPoint _getRandomSpawn;
        
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
                transform.position = _playerMovement.transform.position;
                var enemy = enemyPool.GetFromPool();
                enemy.transform.SetParent(enemyContainer);
                enemy.transform.position = _getRandomSpawn.GetRandomPoint(minPos, maxPos);
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