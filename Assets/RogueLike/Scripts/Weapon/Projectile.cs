using System.Collections;
using RogueLike.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RogueLike.Scripts.Weapon
{
    public abstract class Projectile : MonoBehaviour
    {
        protected WaitForSeconds Timer;
        protected float Damage;

        protected virtual void OnEnable()
        {
            StartCoroutine(TimeToHide());
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out EnemyHealth enemy)) return;
            
            var damage = Random.Range(Damage / 1.5f, Damage * 1.5f);
            if (damage < 1)
                damage = 1;
                
            enemy.TakeDamage(damage);
        }

        private IEnumerator TimeToHide()
        {
            yield return Timer;
            gameObject.SetActive(false);
        }
    }
}