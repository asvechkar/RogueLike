using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Coin : Loot
    {
        private AudioSource _audioSource;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupCoins(1));
            
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;
            _audioSource.Play();
            
            StartCoroutine(DeactivateAfterAudio());
        }
        
        private IEnumerator DeactivateAfterAudio()
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            base.Collect();
        }
    }
}