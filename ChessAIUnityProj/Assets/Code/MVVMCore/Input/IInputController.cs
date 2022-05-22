using System;
using UnityEngine;

namespace MVVMCore.Input
{
    public interface IInputController
    {
        /// <summary>
        /// Fired when mouse left button turns up. Passes mouse world position as argument.
        /// </summary>
        event Action<Vector2> MouseUp;
        
        /// <summary>
        /// Fired when mouse left button pressed down. Passes mouse world position as argument.
        /// </summary>
        event Action<Vector2> MouseDown;
        
        /// <summary>
        /// Fired every time mouse is moved. Passes mouse world position as argument.
        /// </summary>
        event Action<Vector2> MousePositionChanged;
        
        /// <summary>
        /// The mouse world position.
        /// </summary>
        Vector2 MousePosition { get; }
        
        
        float ScreenWidth { get; }
        
        
        void SetCamera(Camera mainCamera);
        
        
        Vector3 ScreenPointToWorld(Vector3 screenPoint);
    }
}