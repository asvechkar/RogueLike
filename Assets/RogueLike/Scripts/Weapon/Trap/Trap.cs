using System.Collections;
using RogueLike.Scripts.Enemy;
using UnityEngine;

namespace RogueLike.Scripts.Weapon.Trap
{
    public class Trap : Projectile
    {
        [SerializeField] private CircleCollider2D trapCollider;
        [SerializeField] private float cooldown = 10f;
        private WaitForSeconds _checkCooldown = new(3f);

        private ProjectileParams _params;

        public void Init(ProjectileParams projectileParams)
        {
            _params = projectileParams;
            Damage = _params.Damage;
        }

        protected override void OnEnable()
        {
            trapCollider.enabled = false;
            StartCoroutine(CheckCooldown());
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

        private void Update()
        {
            cooldown -= Time.deltaTime;
        }

        public void ActivateTrap()
        {
            trapCollider.enabled = true;
        }

        private IEnumerator CheckCooldown()
        {
            while (true)
            {
                if (cooldown < 0)
                {
                    gameObject.SetActive(false);
                }
                
                yield return _checkCooldown;
            }
        }
    }
}