using UnityEngine;

namespace RogueLike.Scripts.Events
{
    public class OnDamageReceived
    {
        public readonly int Damage;
        public readonly Transform Target;

        public OnDamageReceived(Transform target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}