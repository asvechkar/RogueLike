using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.Events.Player;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class Topbar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerLevel;
        [SerializeField] private TextMeshProUGUI playerScore;
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI waves;
        
        private void UpdatePlayerLevel(OnPlayerLevelChanged evt)
        {
            playerLevel.text = $"Level: {evt.Level}";
        }

        private void UpdateWaves(OnWaveChanged evt)
        {
            waves.text = $"Wave: {evt.WaveNumber + 1}";
        }

        private void UpdateCoins(OnCoinsChanged evt)
        {
            StartCoroutine(IncreaseCoins(evt.OldAmount, evt.NewAmount));
        }

        private IEnumerator IncreaseCoins(int oldAmount, int newAmount)
        {
            for (var i = oldAmount; i <= newAmount; i++)
            {
                coins.text = $"Coins: {i}";
                yield return null;
            }
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerLevelChanged>(UpdatePlayerLevel);
            EventBus.Subscribe<OnWaveChanged>(UpdateWaves);
            EventBus.Subscribe<OnCoinsChanged>(UpdateCoins);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(UpdatePlayerLevel);
            EventBus.Unsubscribe<OnWaveChanged>(UpdateWaves);
            EventBus.Unsubscribe<OnCoinsChanged>(UpdateCoins);
        }
    }
}