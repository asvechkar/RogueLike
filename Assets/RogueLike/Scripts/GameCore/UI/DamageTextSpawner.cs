using System;
using System.Collections;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.GameCore.Pool;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RogueLike.Scripts.GameCore.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool textPool;
        
        private readonly WaitForSeconds _wait = new(0.05f);

        private void Awake()
        {
            EventBus.Subscribe<OnDamageReceived>(ShowDamageText);
        }

        private void ShowDamageText(OnDamageReceived evt)
        {
            var target = evt.Target;
            var damage = evt.Damage;
            
            var damageText = textPool.GetFromPool();
            damageText.transform.SetParent(transform);
            damageText.transform.position = target.position + GetDamageTextPosition();

            if (damageText.TryGetComponent(out TextMeshPro damageLabel))
            {
                damageLabel.text = damage.ToString();
                damageLabel.fontSize = Mathf.Clamp(damage, 2f, 15f);
                damageText.SetActive(true);

                StartCoroutine(FadeAnimation(damageLabel, damageText));
            }
        }

        private IEnumerator FadeAnimation(TextMeshPro damageText, GameObject targetEffect)
        {
            var color = damageText.color;
            
            color.a = 1f;

            for (var f = 1f; f >= -0.05f; f-=0.05f)
            {
                damageText.color = color;
                color.a = f;
                yield return _wait;
            }
            
            targetEffect.SetActive(false);
        }

        private Vector3 GetDamageTextPosition()
        {
            return new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
    }
}