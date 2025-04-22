using Reflex.Core;
using RogueLike.Scripts.GameCore.ExperienceSystem;
using UnityEngine;

namespace RogueLike.Scripts.DI
{
    public class PlayerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ExperienceSpawner experienceSpawner;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(experienceSpawner, typeof(ExperienceSpawner));
        }
    }
}