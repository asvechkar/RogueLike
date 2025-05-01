using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.Managers;
using RogueLike.Scripts.Player;
using RogueLike.Scripts.Weapon;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class StatsWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI maxHp;
        [SerializeField] private TextMeshProUGUI currentHp;
        [SerializeField] private TextMeshProUGUI healPoints;
        [SerializeField] private TextMeshProUGUI speed;
        
        [SerializeField] private TextMeshProUGUI fireballLevel;
        [SerializeField] private TextMeshProUGUI auraLevel;
        [SerializeField] private TextMeshProUGUI surikenLevel;
        [SerializeField] private TextMeshProUGUI frostboltLevel;
        [SerializeField] private TextMeshProUGUI trapLevel;
        [SerializeField] private TextMeshProUGUI bowLevel;
        
        private void UpdateHealth(OnPlayerHealthChanged evt)
        {
            maxHp.text = $"Max HP: {evt.MaxHealth}";
            currentHp.text = $"Current HP: {evt.CurrentHealth}";
            healPoints.text = $"Heal: {evt.HealPoints}";
        }
        
        private void UpdateSpeed(OnPlayerSpeedChanged evt)
        {
            speed.text = $"Speed: {evt.Speed}";
        }

        private void UpdateWeaponLevel(OnWeaponLevelChanged evt)
        {
            switch (evt.WeaponType)
            {
                case WeaponType.Fireball:
                    fireballLevel.text = $"Fireball level: {evt.Level}";
                    break;
                case WeaponType.Aura:
                    auraLevel.text = $"Aura level: {evt.Level}";
                    break;
                case WeaponType.Frostbolt:
                    frostboltLevel.text = $"Frostbolt level: {evt.Level}";
                    break;
                case WeaponType.Suriken:
                    surikenLevel.text = $"Suriken level: {evt.Level}";
                    break;
                case WeaponType.Trap:
                    trapLevel.text = $"Trap level: {evt.Level}";
                    break;
                case WeaponType.Bow:
                    bowLevel.text = $"Bow level: {evt.Level}";
                    break;
            }
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerHealthChanged>(UpdateHealth);
            EventBus.Subscribe<OnPlayerSpeedChanged>(UpdateSpeed);
            EventBus.Subscribe<OnWeaponLevelChanged>(UpdateWeaponLevel);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerHealthChanged>(UpdateHealth);
            EventBus.Unsubscribe<OnPlayerSpeedChanged>(UpdateSpeed);
            EventBus.Unsubscribe<OnWeaponLevelChanged>(UpdateWeaponLevel);
        }
    }
}