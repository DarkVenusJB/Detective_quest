using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace Services
{
    public class SceneLoaderService
    {
        private EEnviromentType _currentEnvironment;
        private readonly LoadingScreenView _loadingScreen;
        private bool _isLoading;
        
        public EEnviromentType CurrentEnvironment => _currentEnvironment;
        public bool IsLoading => _isLoading;
        
    
        public SceneLoaderService(EEnviromentType environment, LoadingScreenView loadingScreen)
        {
            _currentEnvironment = environment;
            _loadingScreen = loadingScreen;
        }
    
        public async UniTask LoadScene(EEnviromentType environment)
        {
            if(_currentEnvironment == environment)
                return;

            if (environment == EEnviromentType.Global)
            {
                Debug.LogError("Невозможно загрузить глобальную сцену!");
                return;
            }
         
            _isLoading = true;
            
            var environmentIndex = (int)environment;
            var currentEnvironmentIndex = (int)_currentEnvironment;
        
            if (_currentEnvironment != EEnviromentType.Global)
                await SceneManager.UnloadSceneAsync(currentEnvironmentIndex);
        
            await SceneManager.LoadSceneAsync(environmentIndex, LoadSceneMode.Additive);
        
            _currentEnvironment = environment;
        
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(environmentIndex));

            await UniTask.NextFrame();
            
            _isLoading = false;
        }
    }
}
