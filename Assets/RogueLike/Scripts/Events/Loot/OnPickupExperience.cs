namespace RogueLike.Scripts.Events.Loot
{
    public class OnPickupExperience
    {
        public int ExperienceValue { get; private set; }

        public OnPickupExperience(int experienceValue)
        {
            ExperienceValue = experienceValue;
        }
    }
}