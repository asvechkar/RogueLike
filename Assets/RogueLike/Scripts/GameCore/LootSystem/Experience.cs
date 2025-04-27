using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Experience: Loot
    {
        [SerializeField] private int value;
        [SerializeField] private AudioSource audioSource;
        private readonly float _distanceToPickup = 1.5f;

        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupExperience(value));
            audioSource.Play();
            StartCoroutine(DeactivateAfterAudio());
        }
        
        private System.Collections.IEnumerator DeactivateAfterAudio()
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            base.Collect();
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