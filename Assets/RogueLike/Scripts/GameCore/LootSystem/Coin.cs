using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Coin : Loot
    {
        [SerializeField] private AudioSource audioSource;

        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupCoins(1));
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