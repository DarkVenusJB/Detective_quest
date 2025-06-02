using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Services.Map;
using UnityEngine;
using Zenject;

public class MapEntryPoint : MonoBehaviour
{
    [SerializeField] private int _numberOfHouses = 3;
    [SerializeField] private CanvasGroup _loadingScreen;

    private ISpawnPointService _spawnPointService;
    
    private const float LOADING_WINDOW_DURATION = 0.5f;
    
    [Inject]
    public void Inject(ISpawnPointService spawnPointService)
    {
        _spawnPointService = spawnPointService;
    }
    
    private void Start()
    {
        _loadingScreen.alpha = 1;
        
        DifferedStart();
    }

    private async void DifferedStart()
    {
        await UniTask.WaitWhile(() => _spawnPointService == null);
        
        await UniTask.Delay(500);

        _loadingScreen.DOFade(0, LOADING_WINDOW_DURATION).OnComplete(() =>
        {
            _loadingScreen.gameObject.SetActive(false);
        });
    }
}
