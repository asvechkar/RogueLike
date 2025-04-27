namespace RogueLike.Scripts.Events.Loot
{
    public class OnPickupHeart
    {
        public int Health { get; private set; }

        public OnPickupHeart(int health)
        {
            Health = health;
        }
    }
}