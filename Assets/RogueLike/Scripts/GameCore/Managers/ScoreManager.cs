using System;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Enemy;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }
        
        [Inject] private readonly GameData _gameData;

        private void Awake()
        {
            Score = _gameData.score;
        }

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