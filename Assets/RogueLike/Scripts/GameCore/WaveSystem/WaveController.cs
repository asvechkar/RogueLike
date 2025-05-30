using System.Collections.Generic;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.GameCore.Managers;
using RogueLike.Scripts.GameCore.UI.Game;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.WaveSystem
{
    public class WaveController : MonoBehaviour, IActivate
    {
        [SerializeField] private List<Wave> waves = new();
        
        [Inject] private GameManager gameManager;
        [Inject] private GameOverWindow gameOverWindow;
        [Inject] private SaveManager saveManager;

        private void Start()
        {
            Activate();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnWaveChanged>(NextWave);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnWaveChanged>(NextWave);
        }

        private void NextWave(OnWaveChanged evt)
        {
            if (evt.WaveNumber >= waves.Count - 1)
            {
                gameManager.GameOver();
                saveManager.SaveGame();
                gameOverWindow.SetResult("You won!");
                gameOverWindow.gameObject.SetActive(true);
                
                EventBus.Invoke(new OnGameOver("You won!"));
            }
            else
            {
                if (evt.WaveNumber > 0)
                    waves[evt.WaveNumber - 1].Deactivate();
                
                waves[evt.WaveNumber].Activate();
            }
        }

        public void Activate()
        {
            EventBus.Invoke(new OnWaveChanged(0));
        }

        public void Deactivate()
        {
            waves[^1].Deactivate();
        }
    }
}