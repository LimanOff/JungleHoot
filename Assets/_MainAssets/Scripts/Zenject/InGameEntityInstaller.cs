using Zenject;

public class InGameEntityInstaller : MonoInstaller
{
    public Timer Timer;
    public TimerUI TimerUI;

    public override void InstallBindings()
    {
        Container.Bind<Timer>().FromInstance(Timer).AsSingle();
        Container.Bind<TimerUI>().FromInstance(TimerUI).AsSingle();
        Container.Bind<LoaderNewLevels>().AsSingle().NonLazy();

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Timer.Initialize();
    }
}