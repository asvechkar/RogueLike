using Reflex.Attributes;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public abstract class Loot : MonoBehaviour
    {
        [SerializeField] private AudioClip collectSound;
        
        [Inject] private SoundFxManager soundFxManager;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            var position = transform.position;
            
            soundFxManager.PlaySoundFxClip(collectSound, position);
            gameObject.SetActive(false);
        }
    }
}