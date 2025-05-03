using UnityEngine;
using UnityEngine.Audio;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class SoundMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", volume);
        }
        
        public void SetSoundFXVolume(float volume)
        {
            audioMixer.SetFloat("soundFXVolume", volume);
        }
        
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("musicVolume", volume);
        }
    }
}