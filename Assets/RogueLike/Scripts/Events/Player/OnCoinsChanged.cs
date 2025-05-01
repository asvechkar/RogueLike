namespace RogueLike.Scripts.Events.Player
{
    public class OnCoinsChanged
    {
        public int Amount { get; private set; }
        
        public OnCoinsChanged(int amount)
        {
            Amount = amount;
        }
    }
}