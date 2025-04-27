using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class PauseWindow : MonoBehaviour
    {
        public void OnResumeClicked()
        {
            EventBus.Invoke(new OnGameResumed());
            gameObject.SetActive(false);
        }
    }
}
