using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalSceneInstaller : MonoInstaller
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            DOTween.Init();
        }
        
        public override void InstallBindings()
        {
           
        }
    }
}