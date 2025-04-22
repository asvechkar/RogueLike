using RogueLike.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        [SerializeField] private Image playerHealthImage;
        
        private void OnEnable() => EventBus.Subscribe<OnHealthChanged>(UpdateHealthBar);

        private void OnDisable() => EventBus.Unsubscribe<OnHealthChanged>(UpdateHealthBar);

        private void UpdateHealthBar(OnHealthChanged evt)
        {
            playerHealthImage.fillAmount = Mathf.Clamp01(evt.CurrentHealth / evt.MaxHealth);
        }
    }
}