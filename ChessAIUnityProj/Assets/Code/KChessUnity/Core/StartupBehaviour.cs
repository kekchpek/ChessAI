using KChessUnity.Core.Camera;
using UnityEngine;
using UnityMVVM.ViewManager;
using Zenject;

namespace KChessUnity.Core
{
    public class StartupBehaviour : MonoBehaviour
    {

        [SerializeField] private UnityEngine.Camera _camera;

        private IViewManager _viewManager;
        private ICameraService _cameraService;
        
        [Inject]
        public void Construct(
            ICameraService cameraService,
            IViewManager viewManager)
        {
            _viewManager = viewManager;
            _cameraService = cameraService;
        }
        
        private void Awake()
        {
            _cameraService.SetCamera(_camera);
            _viewManager.Open(ViewLayersIds.Main, ViewNames.MainMenu);
        }
    }
}