using Reflex.Core;
using RogueLike.Scripts.GameCore.LootSystem;
using RogueLike.Scripts.GameCore.UpgradeSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueLike.Scripts.DI
{
    public class SceneInstaller : MonoBehaviour, IInstaller
    {
        [FormerlySerializedAs("experienceSpawner")] [SerializeField] private LootSpawner lootSpawner;
        [SerializeField] private UpgradeWindow upgradeWindow;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(lootSpawner, typeof(LootSpawner));
            builder.AddSingleton(upgradeWindow, typeof(UpgradeWindow));
        }
    }
}