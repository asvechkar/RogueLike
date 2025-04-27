using UnityEngine;

namespace RogueLike.Scripts.GameCore.UpgradeSystem
{
    [CreateAssetMenu(fileName = "NewUpgradeCard", menuName = "Upgrade/Upgrade Card", order = 0)]
    public class UpgradeCard : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private int cost;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;

        public int ID => id;
        public int Cost => cost;
        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
    }
}