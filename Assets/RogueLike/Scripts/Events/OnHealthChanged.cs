namespace RogueLike.Scripts.Events
{
    public class OnHealthChanged
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        
        public OnHealthChanged(float maxHealth, float currentHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }
    }
}