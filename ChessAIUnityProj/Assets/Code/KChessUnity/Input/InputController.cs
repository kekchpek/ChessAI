using System;
using UnityEngine;

namespace KChessUnity.Input
{
    public class InputController : MonoBehaviour, IInputController
    {
        public event Action<Vector2> MouseUp;
        public event Action<Vector2> MouseDown;
        public event Action<Vector2> MousePositionChanged;

        private Camera _camera;
        private (int width, int height) _screenResolution;
        private float _screenScale;
        private Vector3 _bottomLeftScreenPoint;

        
        
        public Vector2 MousePosition => UnityEngine.Input.mousePosition * _screenScale + _bottomLeftScreenPoint;
        public float ScreenWidth => Screen.width;

        public void SetCamera(Camera mainCamera)
        {
            _camera = mainCamera;
        }

        public Vector3 ScreenPointToWorld(Vector3 screenPoint)
        {
            return _camera.ScreenToWorldPoint(screenPoint);
        }

        public void Update()
        {
            if (_screenResolution.width != Screen.width || _screenResolution.height != Screen.height) {
                
                _screenResolution = (Screen.width, Screen.height);
                _bottomLeftScreenPoint = _camera.ScreenToWorldPoint(Vector3.zero);
                var topRightScreenPoint = _camera.ScreenToWorldPoint(new Vector3(_screenResolution.width, _screenResolution.height, 0f));
                _screenScale = (topRightScreenPoint.x - _bottomLeftScreenPoint.x) / _screenResolution.width;
            }

            var mouseWorldPos = UnityEngine.Input.mousePosition * _screenScale + _bottomLeftScreenPoint;
            
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke(mouseWorldPos);
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                MouseDown?.Invoke(mouseWorldPos);
            }
            
            MousePositionChanged?.Invoke(mouseWorldPos);
        }
    }
}
