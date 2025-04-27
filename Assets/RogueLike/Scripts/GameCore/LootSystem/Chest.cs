using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Chest : Loot
    {
        [SerializeField] private AudioSource audioSource;

        protected override void Collect()
        {
            var coins = Random.Range(10, 50);
            EventBus.Invoke(new OnPickupCoins(coins));
            audioSource.Play();
            StartCoroutine(DeactivateAfterAudio());
        }

        private System.Collections.IEnumerator DeactivateAfterAudio()
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            base.Collect();
        }
    }
}