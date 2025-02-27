using System.Collections;
using GameCore.Health;
using UnityEngine;

namespace Player
{
    public class PlayerHealth: ObjectHealth
    {
        private WaitForSeconds _regenerationDelay = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (CurrentHealth <= 0)
            {
                Debug.Log("Player is dead");
            }
        }

        private void Start()
        {
            StartCoroutine(Regenerate());
        }

        private IEnumerator Regenerate()
        {
            while (true)
            {
                TakeHealth(_regenerationValue);
                yield return _regenerationDelay;
            }
        }
    }
}