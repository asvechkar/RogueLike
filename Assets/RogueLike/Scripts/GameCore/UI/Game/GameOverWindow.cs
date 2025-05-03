using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Game;
using RogueLike.Scripts.GameCore.Managers;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI.Game
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;
        
        [Inject] private GameManager gameManager;

        public void SetResult(string result)
        {
            resultText.text = result;
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