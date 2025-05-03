using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Coin : Loot
    {
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;
        
        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupCoins(1));
            
            base.Collect();
        }
    }
}