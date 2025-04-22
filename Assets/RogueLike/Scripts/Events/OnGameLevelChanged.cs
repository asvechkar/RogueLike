namespace RogueLike.Scripts.Events
{
    public class OnGameLevelChanged
    {
        public int LevelNumber {get; private set;}
        public OnGameLevelChanged(int levelNumber)
        {
            LevelNumber = levelNumber;
        }
    }
}