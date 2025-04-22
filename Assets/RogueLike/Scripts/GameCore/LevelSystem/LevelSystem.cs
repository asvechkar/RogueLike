using System.Collections.Generic;
using RogueLike.Scripts.Events;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LevelSystem
{
    public class LevelSystem : MonoBehaviour, IActivate
    {
        [SerializeField] private List<Level> levels = new();

        private void Start()
        {
            Activate();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnGameLevelChanged>(LevelUp);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnGameLevelChanged>(LevelUp);
        }

        private void LevelUp(OnGameLevelChanged evt)
        {
            levels[evt.LevelNumber - 1].Deactivate();
            levels[evt.LevelNumber].Activate();
        }

        public void Activate()
        {
            levels[0].Activate();
        }

        public void Deactivate()
        {
            levels[^1].Deactivate();
        }
    }
}