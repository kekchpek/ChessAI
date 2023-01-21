using UnityEngine;
using UnityEngine.EventSystems;
using UnityMVVM;

namespace KChessUnity.MVVM.Views.Board
{
    public class BoardView : ViewBehaviour<IBoardViewModel>, IPointerClickHandler
    {
        [SerializeField] private Transform _bottomLeftCorner;
        [SerializeField] private Transform _topRightCorner;

        public void OnPointerClick(PointerEventData eventData)
        {
            var topRight = _topRightCorner.position;
            var bottomLeft = _bottomLeftCorner.position;
            var size = Mathf.Abs(bottomLeft.x - topRight.x);
            var rectSize = ((RectTransform)transform).sizeDelta;
            var boardRectPos = (eventData.pointerPressRaycast.worldPosition - bottomLeft) / size * rectSize
                - rectSize / 2f;
            ViewModel.OnBoardClicked(boardRectPos);
        }
    }
}