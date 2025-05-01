using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Loot;
using RogueLike.Scripts.Events.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public class Experience: Loot
    {
        [SerializeField] private int value;
        
        private AudioSource _audioSource;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;
        private readonly float _distanceToPickup = 1.5f;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        protected override void Collect()
        {
            EventBus.Invoke(new OnPickupExperience(value));
            
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;
            _audioSource.Play();
            
            StartCoroutine(DeactivateAfterAudio());
        }
        
        private System.Collections.IEnumerator DeactivateAfterAudio()
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
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