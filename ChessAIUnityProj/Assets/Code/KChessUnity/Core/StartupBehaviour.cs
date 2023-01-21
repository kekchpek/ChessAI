using KChess.Core.API.PlayerFacade;
using KChessUnity.Core.Camera;
using KChessUnity.Models.Startup;
using UnityEngine;
using Zenject;

namespace KChessUnity.Core
{
    public class StartupBehaviour : MonoBehaviour
    {

        [SerializeField] private UnityEngine.Camera _camera;

        private IStartupService _startupService;
        private ICameraService _cameraService;

        private IPlayerFacade _whitePlayerFacade;
        private IPlayerFacade _blackPlayerFacade;
        
        [Inject]
        public void Construct(
            IStartupService startupService,
            ICameraService cameraService)
        {
            _startupService = startupService;
            _cameraService = cameraService;
        }
        
        private void Awake()
        {
            _cameraService.SetCamera(_camera);
            _startupService.StartSingleGame();
        }
    }
}