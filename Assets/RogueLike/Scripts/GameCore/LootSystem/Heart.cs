using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Heart : Loot
    {
        [SerializeField] private int health;
        
        private AudioSource _audioSource;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;
        private ParticleSystem _heartParticleSystem;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _heartParticleSystem = GetComponentInChildren<ParticleSystem>();
        }
        
        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupHeart(health));
            
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;
            _audioSource.Play();
            _heartParticleSystem.transform.position = transform.position;
            _heartParticleSystem.Play();
            
            StartCoroutine(DeactivateAfterAudio());
        }
        
        private System.Collections.IEnumerator DeactivateAfterAudio()
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            base.Collect();
        }
    }
}