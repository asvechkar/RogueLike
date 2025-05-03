using Reflex.Core;
using RogueLike.Scripts.GameCore.Managers;
using UnityEngine;

namespace RogueLike.Scripts.DI
{
    public class MainMenuInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private SoundMixerManager soundMixerManager;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(soundMixerManager, typeof(SoundMixerManager));
        }
    }
}
