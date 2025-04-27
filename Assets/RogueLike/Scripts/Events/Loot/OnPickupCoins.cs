namespace RogueLike.Scripts.Events.Loot
{
    public class OnPickupCoins
    {
        public int Coins { get; private set; }
        
        public OnPickupCoins(int coins)
        {
            Coins = coins;
        }
    }
}