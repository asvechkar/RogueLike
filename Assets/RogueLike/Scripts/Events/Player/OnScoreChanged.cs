namespace RogueLike.Scripts.Events.Player
{
    public class OnScoreChanged
    {
        public int Score { get; private set; }
        
        public OnScoreChanged(int score)
        {
            Score = score;
        }
    }
}