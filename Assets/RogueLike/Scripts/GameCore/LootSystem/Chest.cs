using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Chest : Loot
    {
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        protected override void Collect()
        {
            var coins = Random.Range(10, 50);
            EventBus.Invoke(new OnPickupCoins(coins));
            
            base.Collect();
        }
    }
}