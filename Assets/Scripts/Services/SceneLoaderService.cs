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
            
        }
    
        public async UniTask StartLoadScene(EEnviromentType environment, bool isStartGame = false)
        {
            if(_currentEnvironment == environment)
                return;

            if (environment == EEnviromentType.Global)
            {
                Debug.LogError("Невозможно загрузить глобальную сцену!");
                return;
            }
            
            var environmentIndex = (int)environment;
            var currentEnvironmentIndex = (int)_currentEnvironment;
        
            _isLoading = true;
            
            Debug.Log("Star loading scene" + environment);
            
            _loadingScreen.ShowLoadingScreen(isStartGame);
            
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
        }
        
        
    }
}
