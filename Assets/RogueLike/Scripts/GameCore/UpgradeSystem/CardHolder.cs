using System.Globalization;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Player;
using RogueLike.Scripts.GameCore.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UpgradeSystem
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI skillValueText;
        [SerializeField] private Image icon;
        [SerializeField] private UpgradeCard upgradeCard;
        
        [Inject] private SkillManager skillManager;
        [Inject] private WeaponManager weaponManager;
        [Inject] private PlayerManager playerManager;

        private void Start()
        {
            titleText.text = upgradeCard.Title;
            descriptionText.text = upgradeCard.Description;
            icon.sprite = upgradeCard.Icon;
            skillValueText.text = GetSkillValueText();
        }

        private string GetSkillValueText()
        {
            return upgradeCard.UpgradeType switch
            {
                UpgradeType.Player => playerManager.GetSkillValue(upgradeCard.PlayerSkillType)
                    .ToString(CultureInfo.CurrentCulture),
                UpgradeType.Weapon => weaponManager.GetWeaponLevel(upgradeCard.WeaponType)
                    .ToString(),
                _ => ""
            };
        }

        private void UpdatePlayerSkillValueText(OnPlayerSkillChanged evt)
        {
            skillValueText.text = GetSkillValueText();
        }

        private void UpdateWeaponLevelText(OnWeaponLevelChanged evt)
        {
            skillValueText.text = GetSkillValueText();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnPlayerSkillChanged>(UpdatePlayerSkillValueText);
            EventBus.Subscribe<OnWeaponLevelChanged>(UpdateWeaponLevelText);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnPlayerSkillChanged>(UpdatePlayerSkillValueText);
            EventBus.Unsubscribe<OnWeaponLevelChanged>(UpdateWeaponLevelText);
        }

        public void Upgrade()
        {
            if (skillManager.SkillPoints == 0) return;
            
            switch (upgradeCard.UpgradeType)
            {
                case UpgradeType.Player:
                    skillManager.SpendSkillPoints(1);
                    playerManager.UpgradeSkill(upgradeCard.PlayerSkillType);
                    EventBus.Invoke(new OnPlayerSkillChanged(upgradeCard.PlayerSkillType));
                    break;
                case UpgradeType.Weapon:
                default:
                    if (weaponManager.CanUpgrade(upgradeCard.WeaponType))
                    {
                        skillManager.SpendSkillPoints(1);
                        EventBus.Invoke(new OnWeaponLevelUpdated(upgradeCard.WeaponType));
                    }
                    break;
            }
        }
    }
}