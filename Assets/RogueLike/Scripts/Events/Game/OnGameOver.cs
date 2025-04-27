namespace RogueLike.Scripts.Events.Game
{
    public class OnGameOver
    {
        public string GameOverMessage { get; private set; }
        
        public OnGameOver(string gameOverMessage)
        {
            GameOverMessage = gameOverMessage;
        }
    }
}