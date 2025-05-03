using System.Collections;
using Reflex.Attributes;
using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.InputEvents;
using RogueLike.Scripts.GameCore.Managers;
using RogueLike.Scripts.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UI.Game
{
    public class WeaponButton : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private Slider cooldownSlider;

        [Inject] private WeaponManager weaponManager;
        
        private Button button;
        private float cooldown;
        
        private void Start()
        {
            button = GetComponent<Button>();
        }

        public void OnClicked()
        {
            if (!button.interactable)
                return;
            ActivateWeapon();
        }

        private void ActivateWeapon()
        {
            EventBus.Invoke(new OnAttacked(weaponType));
            button.interactable = false;
            cooldownSlider.gameObject.SetActive(true);
            cooldown = weaponManager.GetWeaponCooldown(weaponType);
            cooldownSlider.value = cooldown;
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            while (cooldown > 0)
            {
                cooldownSlider.value -= Time.deltaTime;
                cooldown -= Time.deltaTime;
                yield return null;
            }
            
            button.interactable = true;
            cooldownSlider.gameObject.SetActive(false);
        }
    }
}
