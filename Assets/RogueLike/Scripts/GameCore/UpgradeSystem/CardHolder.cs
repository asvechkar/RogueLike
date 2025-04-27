using System;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UpgradeSystem
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image icon;
        [SerializeField] private UpgradeCard upgradeCard;
        
        [Inject]
        private UpgradeWindow _upgradeWindow;

        private void Start()
        {
            titleText.text = upgradeCard.Title;
            descriptionText.text = upgradeCard.Description;
            icon.sprite = upgradeCard.Icon;
        }
        
        public void Upgrade()
        {
            _upgradeWindow.Upgrade(upgradeCard.ID);
        }
    }
}