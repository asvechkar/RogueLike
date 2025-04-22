using System;
using UnityEngine;

namespace RogueLike.Scripts.Weapon
{
    [Serializable]
    public class WeaponStats
    {
        [SerializeField] private WeaponData weaponData;

        public float Speed => weaponData.Speed;
        public float Damage => weaponData.Damage;
        public float Range => weaponData.Range;
        public float TimeBetweenAttack => weaponData.TimeBetweenAttack;
        public float Duration => weaponData.Duration;
    }
}