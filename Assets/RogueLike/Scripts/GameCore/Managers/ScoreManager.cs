using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Enemy;
using RogueLike.Scripts.Events.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }

        public void AddScore(int score)
        {
            Score += score;
            EventBus.Invoke(new OnScoreChanged(Score));
        }

        private void UpdateScore(OnEnemyDead evt)
        {
            AddScore(Mathf.FloorToInt(evt.Enemy.MaxHealth));
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnEnemyDead>(UpdateScore);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnEnemyDead>(UpdateScore);
        }
    }
}