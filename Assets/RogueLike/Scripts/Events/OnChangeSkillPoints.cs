namespace RogueLike.Scripts.Events
{
    public class OnChangeSkillPoints
    {
        public int SkillPoints { get; private set; }
        
        public OnChangeSkillPoints(int skillPoints)
        {
            SkillPoints = skillPoints;
        }
    }
}