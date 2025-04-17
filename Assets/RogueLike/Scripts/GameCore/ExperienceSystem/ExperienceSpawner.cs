using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.ExperienceSystem
{
    public class ExperienceSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool experiencePool;

        private void OnEnable()
        {
            EventBus.Subscribe<OnEnemyDeath>(Spawn);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnEnemyDeath>(Spawn);
        }

        private void Spawn(OnEnemyDeath evt)
        {
            var experienceFlask = experiencePool.GetFromPool();
            experienceFlask.transform.SetParent(transform);
            experienceFlask.transform.position = evt.Enemy.transform.position;
        }
    }
}