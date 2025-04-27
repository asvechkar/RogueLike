using UnityEngine;

namespace RogueLike.Scripts.Events.Player
{
    public class OnPlayerMoved
    {
        public Vector3 Position { get; private set; }

        public OnPlayerMoved(Vector3 position)
        {
            Position = position;
        }
    }
}