using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup  _canvasGroup;
        [SerializeField] private float _loadingScreeFadeTime = 0.5f;
        
        private bool _loadingScreenShowed = true;
        
        public bool LoadingScreenShowed => _loadingScreenShowed;

        public void ShowLoadingScreen()
        {
            if (_loadingScreenShowed)
                return;
            
            _canvasGroup.DOFade(0, _loadingScreeFadeTime).OnComplete(() => { _loadingScreenShowed = true; });
            
        }

        public void HideLoadingScreen()
        {
            if(!_loadingScreenShowed)
                return;
            
            _canvasGroup.DOFade(1, _loadingScreeFadeTime).OnComplete(() =>
            {
                
            });
        }
    }
}