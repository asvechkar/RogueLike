using RogueLike.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RogueLike.Scripts.GameCore.UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        [SerializeField] private Image playerHealthImage;
        
        private PlayerHealth _playerHealth;

        private void OnEnable() => _playerHealth.OnHealthChanged += UpdateHealthBar;

        private void OnDisable() => _playerHealth.OnHealthChanged -= UpdateHealthBar;

        private void UpdateHealthBar()
        {
            playerHealthImage.fillAmount = Mathf.Clamp01(_playerHealth.CurrentHealth / _playerHealth.MaxHealth);
        }

        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }
    }
}