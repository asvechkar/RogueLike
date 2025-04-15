using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Enemy;
using UnityEngine;

namespace RogueLike.Scripts.Player.Weapon.Trap
{
    public class Trap : Projectile
    {
        [SerializeField] private CircleCollider2D trapCollider;
        private WaitForSeconds _checkInterval = new WaitForSeconds(3f);
        
        [Inject] private PlayerHealth _playerHealth;
        [Inject] private TrapWeapon _trapWeapon;

        protected override void OnEnable()
        {
            Damage = _trapWeapon.Damage;
            trapCollider.enabled = false;
            StopCoroutine(CheckDistance());
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out EnemyHealth enemy)) return;
            
            enemy.TakeDamage(Damage);
            if (enemy.gameObject.activeSelf)
            {
                enemy.Burn(Damage);
            }
            gameObject.SetActive(false);
        }

        public void ActivateTrap()
        {
            trapCollider.enabled = true;
        }

        private IEnumerator CheckDistance()
        {
            while (true)
            {
                if (Vector3.Distance(transform.position, _playerHealth.gameObject.transform.position) > 15f)
                {
                    gameObject.SetActive(false);
                }
                
                yield return _checkInterval;
            }
        }
    }
}