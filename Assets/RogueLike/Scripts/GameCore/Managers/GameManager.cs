using Reflex.Core;
using RogueLike.Scripts.GameCore.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class GameManager
    {
        public GameDifficultyType Difficulty { get; private set; }
        public void StartGame(GameDifficultyType difficulty)
        {
            Difficulty = difficulty;
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
            var gameData = new GameData();
            var scene = SceneManager.LoadScene("Game", new LoadSceneParameters(LoadSceneMode.Single));
            ReflexSceneManager.PreInstallScene(scene, builder => builder.AddSingleton(gameData));
        }

        public void ExitToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        public void LoadGame(GameData gameData)
        {
            Time.timeScale = 1;
            
            var scene = SceneManager.LoadScene("Game", new LoadSceneParameters(LoadSceneMode.Single));
            ReflexSceneManager.PreInstallScene(scene, builder => builder.AddSingleton(gameData));
        }
    }
}