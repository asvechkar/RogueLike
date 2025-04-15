using Reflex.Core;
using RogueLike.Scripts.Player;
using RogueLike.Scripts.Player.Weapon.Bow;
using RogueLike.Scripts.Player.Weapon.FrostBolt;
using RogueLike.Scripts.Player.Weapon.Suriken;
using RogueLike.Scripts.Player.Weapon.Trap;
using UnityEngine;

namespace RogueLike.Scripts.DI
{
    public class PlayerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private SurikenWeapon surikenWeapon;
        [SerializeField] private FrostBoltWeapon frostBoltWeapon;
        [SerializeField] private TrapWeapon trapWeapon;
        [SerializeField] private BowWeapon bowWeapon;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(playerMovement);
            builder.AddSingleton(playerHealth, typeof(PlayerHealth));
            builder.AddSingleton(surikenWeapon, typeof(SurikenWeapon));
            builder.AddSingleton(frostBoltWeapon, typeof(FrostBoltWeapon));
            builder.AddSingleton(trapWeapon, typeof(TrapWeapon));
            builder.AddSingleton(bowWeapon, typeof(BowWeapon));
        }
    }
}