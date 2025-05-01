using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.GameCore.UpgradeSystem;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class PauseWindow : MonoBehaviour
    {
        [Inject] private UpgradeWindow upgradeWindow;
        
        public void OnResumeClicked()
        {
            EventBus.Invoke(new OnGameResumed());
            gameObject.SetActive(false);
        }

        public void OnUpgradeClicked()
        {
            upgradeWindow.gameObject.SetActive(true);
        }
        
        public void OnRestartClicked()
        {
            EventBus.Invoke(new OnGameStarted());
        }
    }
}
