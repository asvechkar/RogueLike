using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;

        public void SetResult(string result)
        {
            resultText.text = result;
        }
        
        public void OnRestartClicked()
        {
            EventBus.Invoke(new OnGameStarted());
        }
    }
}