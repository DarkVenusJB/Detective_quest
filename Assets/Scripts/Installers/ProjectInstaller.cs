using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("Installing Project Installer");
        
            InstallServices();
        }

        public void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle().NonLazy();
        }
    }
}