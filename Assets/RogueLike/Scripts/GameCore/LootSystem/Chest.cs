using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Chest : Loot
    {
        private AudioSource _audioSource;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        protected override void Collect()
        {
            var coins = Random.Range(10, 50);
            EventBus.Invoke(new OnPickupCoins(coins));
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
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