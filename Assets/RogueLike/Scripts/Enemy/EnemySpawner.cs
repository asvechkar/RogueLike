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
        [SerializeField] private Transform minPos, maxPos;
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private ObjectPool enemyPool;

        [Inject] private GetRandomSpawnPoint _getRandomSpawn;
        
        private WaitForSeconds _interval;
        private Coroutine _spawnCoroutine;
        private Vector3 _playerPosition;

        private void Start()
        {
            _interval = new WaitForSeconds(timeToSpawn);
        }

        private void GetPlayerPosition(OnPlayerMoved evt) => _playerPosition = evt.Position;

        private void OnEnable() => EventBus.Subscribe<OnPlayerMoved>(GetPlayerPosition);

        private void OnDisable() => EventBus.Unsubscribe<OnPlayerMoved>(GetPlayerPosition);

        private IEnumerator Spawn()
        {
            while (true)
            {
                if (_playerPosition != Vector3.zero)
                {
                    transform.position = _playerPosition;
                    var enemy = enemyPool.GetFromPool();
                    enemy.transform.SetParent(enemyContainer);
                    enemy.transform.position = _getRandomSpawn.GetRandomPoint(minPos, maxPos);
                }

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