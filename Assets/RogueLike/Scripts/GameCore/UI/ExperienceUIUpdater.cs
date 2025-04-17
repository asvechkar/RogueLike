using RogueLike.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RogueLike.Scripts.GameCore.UI
{
    public class ExperienceUIUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private Image experienceImage;

        private void Start()
        {
            experienceImage.fillAmount = 0;
            experienceText.text = "1 LVL";
        }

        private void OnEnable()
        {
            EventBus.Subscribe<OnExperienceChanged>(UpdateExperience);
            EventBus.Subscribe<OnPlayerLevelChanged>(UpdateLevelUp);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnExperienceChanged>(UpdateExperience);
            EventBus.Unsubscribe<OnPlayerLevelChanged>(UpdateLevelUp);
        }

        private void UpdateExperience(OnExperienceChanged evt)
        {
            experienceImage.fillAmount = (float)evt.CurrentExperience / evt.ExperienceToUp;
            experienceImage.fillAmount = Mathf.Clamp01(experienceImage.fillAmount);
        }

        private void UpdateLevelUp(OnPlayerLevelChanged evt)
        {
            experienceText.text = $"{evt.Level} LVL";
        }
    }
}