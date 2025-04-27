namespace RogueLike.Scripts.Events.Game
{
    public class OnWaveChanged
    {
        public int WaveNumber {get; private set;}
        public OnWaveChanged(int waveNumber)
        {
            WaveNumber = waveNumber;
        }
    }
}