using UnityEngine;
using Zenject;

public class EntityInstaller : MonoInstaller
{
    public Timer Timer;
    public TimerUI TimerUI;
    public override void InstallBindings()
    {
        Container.Bind<Timer>().FromInstance(Timer).AsSingle();
        Container.Bind<TimerUI>().FromInstance(TimerUI).AsSingle();

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Timer.Initialize();
    }
}