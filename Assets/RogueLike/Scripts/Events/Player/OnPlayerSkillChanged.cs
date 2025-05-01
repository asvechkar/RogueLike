using RogueLike.Scripts.Player;

namespace RogueLike.Scripts.Events.Player
{
    public class OnPlayerSkillChanged
    {
        public PlayerSkillType Skill { get; private set; }
        
        public OnPlayerSkillChanged(PlayerSkillType skill)
        {
            Skill = skill;
        }
    }
}