using System.Collections.Generic;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.WaveSystem
{
    public class WaveController : MonoBehaviour, IActivate
    {
        [SerializeField] private List<Wave> waves = new();

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