namespace RogueLike.Scripts.Events
{
    public class OnPlayerLevelChanged
    {
        public int Level { get; private set; }

        public OnPlayerLevelChanged(int level)
        {
            Level = level;
        }
    }
}