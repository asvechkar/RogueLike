using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapon
{
    public abstract class BaseWeapon: MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
    }
}