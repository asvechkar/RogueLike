using UnityEngine;

namespace RogueLike.Scripts.GameCore.LootSystem
{
    public abstract class Loot : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            gameObject.SetActive(false);
        }
    }
}