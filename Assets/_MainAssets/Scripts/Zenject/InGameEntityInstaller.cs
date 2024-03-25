using ModestTree;
using Zenject;

public class InGameEntityInstaller : MonoInstaller
{
    public Timer Timer;
    public TimerUI TimerUI;

    public override void InstallBindings()
    {
        ValidateComponents();

        Container.Bind<Timer>().FromInstance(Timer).AsSingle().NonLazy();
        Container.Bind<TimerUI>().FromInstance(TimerUI).AsSingle();
        Container.Bind<LoaderNewLevels>().AsSingle().NonLazy();

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Timer.Initialize();
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(Timer, "(InGameEntityInstaller/ValidateComponents) Компонент Timer не был задан");
        Assert.IsNotNull(TimerUI, "(InGameEntityInstaller/ValidateComponents) Компонент TimerUI не был задан");
    }
}