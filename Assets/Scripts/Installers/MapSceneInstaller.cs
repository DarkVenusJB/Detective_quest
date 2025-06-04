using DG.Tweening;
using Services.Map;
using UnityEngine;
using Zenject;

public class MapSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallServices();
    }

    private void InstallServices()
    {
        Container.BindInterfacesAndSelfTo<SpawnPointService>().AsSingle().NonLazy();
    }
}