using RogueLike.Scripts.Events;
using RogueLike.Scripts.Events.Player;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UI.Game
{
    public class ExperienceProgressbar : MonoBehaviour
    {
        [SerializeField] private Image experienceImage;

        private void Start()
        {
            experienceImage.fillAmount = 0;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnExperienceChanged>(UpdateExperience);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnExperienceChanged>(UpdateExperience);
        }

        private void UpdateExperience(OnExperienceChanged evt)
        {
            experienceImage.fillAmount = (float)evt.CurrentExperience / evt.ExperienceToUp;
            experienceImage.fillAmount = Mathf.Clamp01(experienceImage.fillAmount);
        }

        
    }
}