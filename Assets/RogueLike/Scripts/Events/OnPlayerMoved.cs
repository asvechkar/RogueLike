using UnityEngine;

namespace RogueLike.Scripts.Events
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