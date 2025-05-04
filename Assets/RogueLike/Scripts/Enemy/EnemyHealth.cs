using System;
using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Enemy;
using RogueLike.Scripts.GameCore;
using RogueLike.Scripts.GameCore.Health;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.Enemy
{
    public class EnemyHealth: ObjectHealth
    {
        private readonly WaitForSeconds _damageTick = new(1f);
        
        [Inject] private GameManager _gameManager;

        private void Start()
        {
            switch (_gameManager.Difficulty)
            {
                case GameDifficultyType.Easy:
                    default:
                    SetMaxHealth(MaxHealth * 1);
                    break;
                case GameDifficultyType.Normal:
                    SetMaxHealth(MaxHealth * 2);
                    break;
                case GameDifficultyType.Hard:
                    SetMaxHealth(MaxHealth * 3);
                    break;
            }
        }

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

            var tickDamage = damage / 3f;
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