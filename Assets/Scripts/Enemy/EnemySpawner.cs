using System.Collections;
using GameCore;
using GameCore.Pool;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour, IActivate
    {
        [SerializeField] private float timeToSpawn;
        [SerializeField] private Transform minPos, maxPos;
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private ObjectPool enemyPool;

        private PlayerMovement _playerMovement;
        private WaitForSeconds _interval;
        private GetRandomSpawnPoint _getRandomSpawn;
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

        [Inject]
        private void Construct(GetRandomSpawnPoint getRandomSpawn, PlayerMovement playerMovement)
        {
            _getRandomSpawn = getRandomSpawn;
            _playerMovement = playerMovement;
        }
    }
}