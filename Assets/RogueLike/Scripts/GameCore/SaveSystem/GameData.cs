namespace RogueLike.Scripts.GameCore.SaveSystem
{
    [System.Serializable]
    public struct GameData
    {
        public PlayerData playerData;
        public WeaponData WeaponData;
        public GameDifficultyType difficulty;
        public int skillPoints;
        public int coins;
        public int score;
    }
}