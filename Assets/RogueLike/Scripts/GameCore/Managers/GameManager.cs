using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.GameCore.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private GameOverWindow gameOverWindow;

        private void StartGame(OnGameStarted evt)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("WorldScene");
        }
        
        private void PauseGame(OnGamePaused evt)
        {
            Time.timeScale = 0;
        }
        
        private void ResumeGame(OnGameResumed evt)
        {
            Time.timeScale = 1;
        }

        private void GameOver(OnGameOver evt)
        {
            Time.timeScale = 0;
            gameOverWindow.SetResult(evt.GameOverMessage);
            gameOverWindow.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnGamePaused>(PauseGame);
            EventBus.Subscribe<OnGameResumed>(ResumeGame);
            EventBus.Subscribe<OnGameOver>(GameOver);
            EventBus.Subscribe<OnGameStarted>(StartGame);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnGamePaused>(PauseGame);
            EventBus.Unsubscribe<OnGameResumed>(ResumeGame);
            EventBus.Unsubscribe<OnGameOver>(GameOver);
            EventBus.Unsubscribe<OnGameStarted>(StartGame);
        }
    }
}