using Cysharp.Threading.Tasks;
using Data;
using Services;
using UnityEngine;
using View;
using Zenject;

namespace EntryPoints
{
    public class StartEntryPoint : MonoBehaviour
    {
        [Inject] private ISceneLoaderService  _sceneLoaderService;
        
        public void Start()
        {
            DifferedStart().Forget();
        }

        private async UniTask DifferedStart()
        {
            await UniTask.Delay(100);
            
            await UniTask.WaitWhile(() => _sceneLoaderService == null);
            
            await _sceneLoaderService.StartLoadingGlobalScene();
        }
    }
}