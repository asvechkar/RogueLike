using UnityEngine;
using UnityEngine.SceneManagement;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class GameManager
    {
        public void StartGame(GameDifficultyType difficulty)
        {
            RestartGame();
        }
        
        public void PauseGame()
        {
            Time.timeScale = 0;
        }
        
        public void ResumeGame()
        {
            Time.timeScale = 1;
        }

        public void GameOver()
        {
            Time.timeScale = 0;
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }

        public void ExitToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        public void LoadGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }
    }
}