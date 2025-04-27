using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.WaveSystem
{
    public class GameTimer : MonoBehaviour, IActivate
    {
        [SerializeField] private TMP_Text gameTimerText;
        
        private readonly WaitForSeconds _tick = new(1f);
        private Coroutine _timerCoroutine;
        private int _seconds, _minutes;

        private void Start()
        {
            Activate();
        }

        public void Activate()
        {
            _timerCoroutine = StartCoroutine(Timer());
        }

        public void Deactivate()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _seconds++;

                if (_seconds >= 60)
                {
                    _seconds = 0;
                    _minutes++;
                    EventBus.Invoke(new OnWaveChanged(_minutes));
                }

                TimeTextFormat();
                yield return _tick;
            }
        }

        private void TimeTextFormat()
        {
            gameTimerText.SetText($"{_minutes:D2}:{_seconds:D2}");
        }
    }
}