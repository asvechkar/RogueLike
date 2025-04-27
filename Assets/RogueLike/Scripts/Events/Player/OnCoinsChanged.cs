namespace RogueLike.Scripts.Events.Player
{
    public class OnCoinsChanged
    {
        public int NewAmount { get; private set; }
        public int OldAmount { get; private set; }
        
        public OnCoinsChanged(int oldAmount, int newAmount)
        {
            NewAmount = newAmount;
            OldAmount = oldAmount;
        }
    }
}