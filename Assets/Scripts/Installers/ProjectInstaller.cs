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
        Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle().NonLazy();
    }
}