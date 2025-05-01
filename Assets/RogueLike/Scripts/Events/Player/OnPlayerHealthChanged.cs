namespace RogueLike.Scripts.Events.Player
{
    public class OnPlayerHealthChanged
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public float HealPoints { get; private set; }
        
        public OnPlayerHealthChanged(float currentHealth, float maxHealth, float healPoints)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
            HealPoints = healPoints;
        }
    }
}