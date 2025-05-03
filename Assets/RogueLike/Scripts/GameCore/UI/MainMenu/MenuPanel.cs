using Reflex.Attributes;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI.MainMenu
{
    public class MenuPanel : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        
        public void OnEasyClicked()
        {
            gameManager.StartGame(GameDifficultyType.Easy);
        }
        
        public void OnNormalClicked()
        {
            gameManager.StartGame(GameDifficultyType.Normal);
        }
        
        public void OnHardClicked()
        {
            gameManager.StartGame(GameDifficultyType.Hard);
        }
        
        public void OnQuitClicked()
        {
            Application.Quit();
        }
        
        public void OnLoadGameClicked()
        {
            gameManager.LoadGame();
        }
    }
}