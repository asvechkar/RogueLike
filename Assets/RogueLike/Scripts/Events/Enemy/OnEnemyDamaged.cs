using UnityEngine;

namespace RogueLike.Scripts.Events.Enemy
{
    public class OnEnemyDamaged
    {
        public readonly int Damage;
        public readonly Transform Target;

        public OnEnemyDamaged(Transform target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}