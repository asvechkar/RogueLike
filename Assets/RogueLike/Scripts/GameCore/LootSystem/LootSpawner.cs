using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore.Pool;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool heartPool;
        [SerializeField] private ObjectPool coinPool;
        [SerializeField] private ObjectPool experiencePool;
        [SerializeField] private ObjectPool chestPool;

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
            var chance = Random.Range(0f, 100f);

            var selectedPool = chance switch
            {
                <= 15f => chestPool,
                <= 55f => experiencePool,
                <= 85f => coinPool,
                _ => heartPool
            };

            var loot = selectedPool.GetFromPool();
            loot.transform.SetParent(transform);
            loot.transform.position = evt.Enemy.transform.position;
        }
    }
}