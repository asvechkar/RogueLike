using Reflex.Core;
using RogueLike.Scripts.GameCore;
using UnityEngine;

namespace RogueLike.Scripts.DI
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(GetRandomSpawnPoint));
        }
    }
}