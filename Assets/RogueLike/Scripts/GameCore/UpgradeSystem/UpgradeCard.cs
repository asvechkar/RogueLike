using RogueLike.Scripts.Player;
using RogueLike.Scripts.Weapon;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.UpgradeSystem
{
    [CreateAssetMenu(fileName = "NewUpgradeCard", menuName = "Upgrade/Upgrade Card", order = 0)]
    public class UpgradeCard : ScriptableObject
    {
        [SerializeField] private int cost;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private PlayerSkillType playerSkillType;

        public int Cost => cost;
        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;

        public UpgradeType UpgradeType => upgradeType;
        public WeaponType WeaponType => weaponType;
        public PlayerSkillType PlayerSkillType => playerSkillType;
    }
}