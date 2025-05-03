using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Heart : Loot
    {
        [SerializeField] private int health;
        
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;
        private ParticleSystem _heartParticleSystem;
        
        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _heartParticleSystem = GetComponentInChildren<ParticleSystem>();
        }
        
        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupHeart(health));
            
            base.Collect();
        }
    }
}