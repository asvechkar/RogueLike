namespace RogueLike.Scripts.Events
{
    public class OnExperiencePickup
    {
        public int ExperienceValue { get; private set; }

        public OnExperiencePickup(int experienceValue)
        {
            ExperienceValue = experienceValue;
        }
    }
}