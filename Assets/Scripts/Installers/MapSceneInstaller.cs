using DG.Tweening;
using Services.Map;
using UnityEngine;
using Zenject;

public class MapSceneInstaller : MonoInstaller
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
    }
    
    public override void InstallBindings()
    {
        InstallServices();
    }

    private void InstallServices()
    {
        Container.BindInterfacesAndSelfTo<SpawnPointService>().AsSingle().NonLazy();
    }
}