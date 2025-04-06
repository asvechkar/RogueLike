namespace RogueLike.Scripts.GameCore.Health
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void TakeHealth(float health);
    }
}