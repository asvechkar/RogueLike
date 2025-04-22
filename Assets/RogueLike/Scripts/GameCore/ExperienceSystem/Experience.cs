using RogueLike.Scripts.Events;
using RogueLike.Scripts.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.ExperienceSystem
{
    public class Experience: MonoBehaviour
    {
        [SerializeField] private int value;
        
        private float _distanceToPickup = 1.5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                EventBus.Invoke(new OnExperiencePickup(value));
                gameObject.SetActive(false);
            }
        }
        
        private void OnEnable() => EventBus.Subscribe<OnPlayerMoved>(MoveToPlayer);

        private void OnDisable() => EventBus.Unsubscribe<OnPlayerMoved>(MoveToPlayer);

        private void MoveToPlayer(OnPlayerMoved evt)
        {
            if (Vector3.Distance(transform.position, evt.Position) <= _distanceToPickup)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    evt.Position, 
                    2f * Time.deltaTime
                    );
            }
        }
    }
}