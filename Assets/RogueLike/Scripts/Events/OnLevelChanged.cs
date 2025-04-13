namespace RogueLike.Scripts.Events
{
    public class OnLevelChanged
    {
        public int LevelNumber {get; private set;}
        public OnLevelChanged(int levelNumber)
        {
            LevelNumber = levelNumber;
        }
    }
}