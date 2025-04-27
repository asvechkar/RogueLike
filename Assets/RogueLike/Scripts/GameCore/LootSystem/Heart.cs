using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Heart : Loot
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private int health;
        [SerializeField] private ParticleSystem heartParticleSystem;
        
        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupHeart(health));
            audioSource.Play();
            heartParticleSystem.transform.position = transform.position;
            heartParticleSystem.Play();
            StartCoroutine(DeactivateAfterAudio());
        }
        
        private System.Collections.IEnumerator DeactivateAfterAudio()
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            base.Collect();
        }
    }
}