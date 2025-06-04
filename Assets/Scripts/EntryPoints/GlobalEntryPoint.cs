using System;
using Cysharp.Threading.Tasks;
using Data;
using Services;
using UnityEngine;
using Zenject;

namespace EntryPoints
{
    public class GlobalEntryPoint : MonoBehaviour
    {
        [Inject] private SceneLoaderService  _sceneLoaderService;
        
        public void Start()
        {
            DifferedStart().Forget();
        }

        private async UniTask DifferedStart()
        {
            await _sceneLoaderService.LoadScene(EEnviromentType.Map);
        }
    }
}