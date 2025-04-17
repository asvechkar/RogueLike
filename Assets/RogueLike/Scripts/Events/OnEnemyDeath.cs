using RogueLike.Scripts.Enemy;

namespace RogueLike.Scripts.Events
{
    public class OnEnemyDeath
    {
        public EnemyHealth Enemy { get; private set; }
        public OnEnemyDeath(EnemyHealth enemy)
        {
            Enemy = enemy;
        }
    }
}