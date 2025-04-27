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
    }
}