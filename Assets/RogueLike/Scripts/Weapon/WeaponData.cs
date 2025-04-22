using UnityEngine;

namespace RogueLike.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float range;
        [SerializeField] private float timeBetweenAttack;
        [SerializeField] private float duration;

        public float Speed => speed;
        public float Damage => damage;
        public float Range => range;
        public float TimeBetweenAttack => timeBetweenAttack;
        public float Duration => duration;
    }
}