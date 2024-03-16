using Zenject;

public class MainMenuEntityInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputController>().AsSingle().NonLazy();
    }
}
