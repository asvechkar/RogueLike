using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject pauseWindow;
        
        public void OnClicked()
        {
            pauseWindow.SetActive(!pauseWindow.activeSelf);
            EventBus.Invoke(new OnGamePaused());
        }
    }
}
