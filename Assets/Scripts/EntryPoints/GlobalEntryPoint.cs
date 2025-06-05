using Cysharp.Threading.Tasks;
using Data;
using Services;
using UnityEngine;
using View;
using Zenject;

namespace EntryPoints
{
    public class GlobalEntryPoint : MonoBehaviour
    {
        [SerializeField] private LoadingScreenView _loadingScreen;
        
        [Inject] private ISceneLoaderService  _sceneLoaderService;
        
        public void Start()
        {
            _sceneLoaderService.Init(EEnviromentType.Global, _loadingScreen);
            
            DifferedStart().Forget();
        }

        private async UniTask DifferedStart()
        {
            await UniTask.WaitWhile(() => _sceneLoaderService == null);
            
            await _sceneLoaderService.StartLoadScene(EEnviromentType.Map);
        }
    }
}