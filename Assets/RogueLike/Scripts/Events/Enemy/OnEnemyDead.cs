using RogueLike.Scripts.Enemy;

namespace RogueLike.Scripts.Events.Enemy
{
    public class OnEnemyDead
    {
        public EnemyHealth Enemy { get; private set; }
        public OnEnemyDead(EnemyHealth enemy)
        {
            Enemy = enemy;
        }
    }
}