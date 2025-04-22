using UnityEngine;

namespace RogueLike.Scripts.Events.InputEvents
{
    public class OnMoved
    {
        public Vector2 Position { get; private set; }

        public OnMoved(Vector2 position)
        {
            Position = position;
        }
    }
}