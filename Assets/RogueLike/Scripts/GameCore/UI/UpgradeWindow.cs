using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore.Managers;
using TMPro;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UI
{
    public class UpgradeWindow : MonoBehaviour
    {
        [Inject] private PauseWindow pauseWindow;
        [Inject] private SkillManager skillManager;
        [Inject] private CoinManager coinManager;
        
        [SerializeField] private TextMeshProUGUI title;

        private void OnEnable()
        {
            title.text = $"Upgrade skills ({skillManager.SkillPoints} points)";
            EventBus.Subscribe<OnChangeSkillPoints>(UpdateSkillPointsText);
        }
        
        private void OnDisable()
        {
            EventBus.Unsubscribe<OnChangeSkillPoints>(UpdateSkillPointsText);
        }

        private void UpdateSkillPointsText(OnChangeSkillPoints evt)
        {
            title.text = $"Upgrade skills ({evt.SkillPoints} points)";
        }

        public void OnBuyClicked()
        {
            if (coinManager.Coins < 50) return;
            
            coinManager.SpendCoins(50);
            skillManager.AddSkillPoints(1);
        }

        public void OnCloseClicked()
        {
            gameObject.SetActive(false);
            pauseWindow.gameObject.SetActive(true);
        }
    }
}