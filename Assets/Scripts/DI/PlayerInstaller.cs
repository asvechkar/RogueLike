using Player;
using Player.Weapon.Bow;
using Player.Weapon.FrostBolt;
using Player.Weapon.Suriken;
using Player.Weapon.Trap;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private SurikenWeapon surikenWeapon;
        [SerializeField] private FrostBoltWeapon frostBoltWeapon;
        [SerializeField] private TrapWeapon trapWeapon;
        [SerializeField] private BowWeapon bowWeapon;

        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle().NonLazy();
            Container.Bind<SurikenWeapon>().FromInstance(surikenWeapon).AsSingle().NonLazy();
            Container.Bind<FrostBoltWeapon>().FromInstance(frostBoltWeapon).AsSingle().NonLazy();
            Container.Bind<TrapWeapon>().FromInstance(trapWeapon).AsSingle().NonLazy();
            Container.Bind<BowWeapon>().FromInstance(bowWeapon).AsSingle().NonLazy();
        }
    }
}