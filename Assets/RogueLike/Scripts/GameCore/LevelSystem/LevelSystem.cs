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
            EventBus.Subscribe<OnLevelChanged>(LevelUp);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnLevelChanged>(LevelUp);
        }

        private void LevelUp(OnLevelChanged evt)
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