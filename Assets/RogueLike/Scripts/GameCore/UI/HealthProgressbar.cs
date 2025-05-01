using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Player;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UI
{
    public class HealthProgressbar : MonoBehaviour
    {
        [SerializeField] private Image playerHealthImage;
        
        private void OnEnable() => EventBus.Subscribe<OnPlayerHealthChanged>(UpdateHealthBar);

        private void OnDisable() => EventBus.Unsubscribe<OnPlayerHealthChanged>(UpdateHealthBar);

        private void UpdateHealthBar(OnPlayerHealthChanged evt)
        {
            playerHealthImage.fillAmount = Mathf.Clamp01(evt.CurrentHealth / evt.MaxHealth);
        }
    }
}