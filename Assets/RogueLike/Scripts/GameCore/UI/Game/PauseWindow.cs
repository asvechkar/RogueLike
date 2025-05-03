using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI.Game
{
    public class PauseWindow : MonoBehaviour
    {
        [Inject] private UpgradeWindow upgradeWindow;
        [Inject] private GameManager gameManager;
        
        public void OnResumeClicked()
        {
            gameManager.ResumeGame();
            EventBus.Invoke(new OnGameResumed());
            gameObject.SetActive(false);
        }

        public void OnUpgradeClicked()
        {
            upgradeWindow.gameObject.SetActive(true);
        }
        
        public void OnRestartClicked()
        {
            gameManager.RestartGame();
        }
        
        public void OnMainMenuClicked()
        {
            gameManager.ExitToMainMenu();
        }
    }
}
