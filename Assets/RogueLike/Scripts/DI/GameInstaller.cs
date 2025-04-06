using RogueLike.Scripts.GameCore;
using Zenject;

namespace RogueLike.Scripts.DI
{
    public class GameInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
        }
    }
}