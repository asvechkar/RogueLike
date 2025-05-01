using Reflex.Core;
using RogueLike.Scripts.GameCore.LootSystem;
using RogueLike.Scripts.GameCore.Managers;
using RogueLike.Scripts.GameCore.UI;
using UnityEngine;

namespace RogueLike.Scripts.DI
{
    public class SceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private SkillManager skillManager;
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private CoinManager coinManager;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private LootSpawner lootSpawner;
        [SerializeField] private DamageTextSpawner damageTextSpawner;
        [SerializeField] private UpgradeWindow upgradeWindow;
        [SerializeField] private GameOverWindow gameOverWindow;
        [SerializeField] private PauseWindow pauseWindow;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(skillManager, typeof(SkillManager));
            builder.AddSingleton(weaponManager, typeof(WeaponManager));
            builder.AddSingleton(scoreManager, typeof(ScoreManager));
            builder.AddSingleton(coinManager, typeof(CoinManager));
            builder.AddSingleton(playerManager, typeof(PlayerManager));
            builder.AddSingleton(lootSpawner, typeof(LootSpawner));
            builder.AddSingleton(damageTextSpawner, typeof(DamageTextSpawner));
            builder.AddSingleton(pauseWindow, typeof(PauseWindow));
            builder.AddSingleton(upgradeWindow, typeof(UpgradeWindow));
            builder.AddSingleton(gameOverWindow, typeof(GameOverWindow));
        }
    }
}