using System;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Player;
using UnityEngine;

namespace RogueLike.Scripts.GameCore.ExperienceSystem
{
    public class Experience: MonoBehaviour
    {
        [SerializeField] private int value;
        [Inject] private PlayerHealth playerHealth;
        
        private float _distanceToPickup = 1.5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                EventBus.Invoke(new OnExperiencePickup(value));
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, playerHealth.transform.position) <= _distanceToPickup)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    playerHealth.transform.position, 
                    2f * Time.deltaTime
                    );
            }
        }
    }
}