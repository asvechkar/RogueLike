using System.Collections.Generic;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Weapon;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI.Game
{
    public class WeaponBar : MonoBehaviour
    {
        [SerializeField] private List<WeaponButton> weapons = new();
        
        private void ListenActivateWeapon(OnWeaponActivate evt)
        {
            weapons[evt.ButtonNumber - 1].OnClicked();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnWeaponActivate>(ListenActivateWeapon);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnWeaponActivate>(ListenActivateWeapon);
        }
    }
}