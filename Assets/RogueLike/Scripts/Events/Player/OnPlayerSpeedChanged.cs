namespace RogueLike.Scripts.Events.Player
{
    public class OnPlayerSpeedChanged
    {
        public float Speed { get; private set; }
        
        public OnPlayerSpeedChanged(float speed)
        {
            Speed = speed;
        }
    }
}