using Cysharp.Threading.Tasks;
using Data;
using View;

namespace Services
{
    public interface ISceneLoaderService
    {
        EEnviromentType CurrentEnvironment { get; }
        bool IsLoading { get; }
        void Init(EEnviromentType environment, LoadingScreenView loadingScreen);
        UniTask StartLoadingGlobalScene();
        UniTask StartLoadScene(EEnviromentType environment);
        UniTask CompleteLoadScene();
    }
}