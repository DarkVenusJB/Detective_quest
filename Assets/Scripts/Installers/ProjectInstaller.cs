using Services;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallServices();
    }

    public void InstallServices()
    {
        Container.Bind<AudioService>().AsSingle().NonLazy();
        Container.Bind<SceneLoaderService>().AsSingle().NonLazy();
    }
}