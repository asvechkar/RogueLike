using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.Events.Player;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI.Game
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

        private void UpdateScore(OnScoreChanged evt)
        {
            playerScore.text = $"Score: {evt.Score}";
        }

        private void UpdateCoins(OnCoinsChanged evt)
        {
            coins.text = $"Coins: {evt.Amount}";
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerLevelChanged>(UpdatePlayerLevel);
            EventBus.Subscribe<OnWaveChanged>(UpdateWaves);
            EventBus.Subscribe<OnCoinsChanged>(UpdateCoins);
            EventBus.Subscribe<OnScoreChanged>(UpdateScore);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerLevelChanged>(UpdatePlayerLevel);
            EventBus.Unsubscribe<OnWaveChanged>(UpdateWaves);
            EventBus.Unsubscribe<OnCoinsChanged>(UpdateCoins);
            EventBus.Unsubscribe<OnScoreChanged>(UpdateScore);
        }
    }
}