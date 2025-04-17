namespace RogueLike.Scripts.Events
{
    public class OnExperienceChanged
    {
        public int CurrentExperience { get; private set; }

        public int ExperienceToUp { get; private set; }

        public OnExperienceChanged(int currentExperience, int experienceToUp)
        {
            CurrentExperience = currentExperience;
            ExperienceToUp = experienceToUp;
        }
    }
}