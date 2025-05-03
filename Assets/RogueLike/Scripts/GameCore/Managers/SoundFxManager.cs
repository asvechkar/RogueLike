using UnityEngine;

namespace RogueLike.Scripts.GameCore.Managers
{
    public class SoundFxManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void PlaySoundFxClip(AudioClip clip, Vector3 position, float volume = 1f)
        {
            var source = Instantiate(_audioSource, position, Quaternion.identity);
            
            source.clip = clip;
            source.volume = volume;
            source.Play();
            var clipLength = clip.length;
            Destroy(source.gameObject, clipLength);
        }
    }
}