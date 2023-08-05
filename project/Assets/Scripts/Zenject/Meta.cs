using Zenject;

public class Meta : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LocatorManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<WalletManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TileTools>().AsSingle();
        Container.BindInterfacesAndSelfTo<WorldController>().AsSingle();
        Container.BindInterfacesAndSelfTo<BuferItemController>().AsSingle();
        Container.BindInterfacesAndSelfTo<HeroSkillsMathService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemySkillsMathService>().AsSingle();

    }
}