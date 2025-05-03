using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI.Game
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject pauseWindow;
        
        [Inject] private GameManager gameManager;
        
        public void OnClicked()
        {
            gameManager.PauseGame();
            pauseWindow.SetActive(!pauseWindow.activeSelf);
            EventBus.Invoke(new OnGamePaused());
        }
    }
}
