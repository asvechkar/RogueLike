using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Enemy;
using RogueLike.Scripts.GameCore.Health;
using UnityEngine;

namespace RogueLike.Scripts.Enemy
{
    public class EnemyHealth: ObjectHealth
    {
        private readonly WaitForSeconds _damageTick = new(1f);
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            EventBus.Invoke(new OnEnemyDamaged(transform, (int)damage));

            if (CurrentHealth <= 0)
            {
                EventBus.Invoke(new OnEnemyDead(this));
                gameObject.SetActive(false);
            }
        }
        
        public void Burn(float damage) => StartCoroutine(StartBurn(damage));

        private IEnumerator StartBurn(float damage)
        {
            if (gameObject.activeSelf == false)
            {
                yield break;
            }

            float tickDamage = damage / 3f;
            if (tickDamage < 1f)
            {
                tickDamage = 1f;
            }
            
            var roundDamage = Mathf.Round(tickDamage);

            for (int i = 0; i < 5; i++)
            {
                TakeDamage(roundDamage);
                yield return _damageTick;
            }
        }
    }
}