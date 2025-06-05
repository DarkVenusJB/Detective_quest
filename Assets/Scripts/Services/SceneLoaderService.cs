using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private EEnviromentType _currentEnvironment;
        private LoadingScreenView _loadingScreen;
        private bool _isLoading;
        
        public EEnviromentType CurrentEnvironment => _currentEnvironment;
        public bool IsLoading => _isLoading;


        public void Init(EEnviromentType environment, LoadingScreenView loadingScreen)
        {
            _currentEnvironment = environment;
            _loadingScreen = loadingScreen;
            
            
            _loadingScreen.Init();
        }
        
        public async UniTask StartLoadingGlobalScene()
        {
            _isLoading = true;

            if (!Application.CanStreamedLevelBeLoaded((int)EEnviromentType.Global))
            {
                Debug.LogError($"Scene with index {(int)EEnviromentType.Global} cannot be loaded!");
                _isLoading = false;
                return;
            }
            
            _currentEnvironment = EEnviromentType.Global;
            
            await SceneManager.LoadSceneAsync((int)EEnviromentType.Global, LoadSceneMode.Additive);
            await SceneManager.UnloadSceneAsync((int)EEnviromentType.Start);
        }
    
        public async UniTask StartLoadScene(EEnviromentType environment)
        {
            if(_currentEnvironment == environment)
                return;
            
            var environmentIndex = (int)environment;
            var currentEnvironmentIndex = (int)_currentEnvironment;
        
            _isLoading = true;
            
            if (!Application.CanStreamedLevelBeLoaded(environmentIndex))
            {
                Debug.LogError($"Scene with index {environmentIndex} cannot be loaded!");
                _isLoading = false;
                return;
            }
            
            Debug.Log($"Start loading scene: {environment}");
            
            _loadingScreen.ShowLoadingScreen();
            
            await UniTask.WaitUntil(()=>_loadingScreen.LoadingScreenShowed);
            
            if (_currentEnvironment != EEnviromentType.Global)
                await SceneManager.UnloadSceneAsync(currentEnvironmentIndex);
        
            await SceneManager.LoadSceneAsync(environmentIndex, LoadSceneMode.Additive);
        
            _currentEnvironment = environment;
        
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(environmentIndex));

            await UniTask.NextFrame();
            
            _isLoading = false;
        }
        
        
        public async UniTask CompleteLoadScene()
        {
            await UniTask.WaitWhile(() => IsLoading);
            
            _loadingScreen.HideLoadingScreen();
            
            Debug.Log($"Complete loading scene: {_currentEnvironment}");
        }
        
        
    }
}
