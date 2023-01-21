using System;
using KChessUnity.Core.Screen;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityMVVM;
using Zenject;

namespace KChessUnity.MVVM.Views.Piece
{
    public class PieceView : ViewBehaviour<IPieceViewModel>, IPointerDownHandler, IPointerUpHandler
    {

        [SerializeField] private float _dragMinTime;
        [SerializeField] private Image _image;

        private float? _pointerDownTime;
        private bool _dragging;

        private IScreenAdapter _screenAdapter;

        [Inject]
        public void Construct(IScreenAdapter screenAdapter)
        {
            _screenAdapter = screenAdapter;
        }

        private void Reset()
        {
            _image = GetComponent<Image>();
            if (_image is null)
            {
                _image = GetComponentInChildren<Image>();
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            ViewModel!.Position.Bind(OnPositionChanged);
            ViewModel.Image.Bind(OnImageChanged);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDownTime = Time.fixedTime;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_dragging)
            {
                ViewModel.OnPieceDragEnd(((RectTransform)transform).anchoredPosition);
            }
            _dragging = false;
            if (_pointerDownTime.HasValue)
            {
                if (Time.fixedTime - _pointerDownTime < _dragMinTime)
                {
                    ViewModel.OnPieceClicked();
                }
                _pointerDownTime = null;
            }
            OnPositionChanged(ViewModel.Position.Value);
        }

        private void Update()
        {
            if (_pointerDownTime.HasValue && Time.fixedTime - _pointerDownTime >= _dragMinTime)
            {
                if (!_dragging)
                {
                    ViewModel.OnPieceDragStart();
                }
                _dragging = true;
                var transformCatch = transform;
                var maxIndex = transformCatch.childCount - 1;
                if (transformCatch.GetSiblingIndex() != maxIndex)
                {
                    transformCatch.SetSiblingIndex(maxIndex);
                }

                transform.position =
                    (Vector2)_screenAdapter.ScreenPointToWorld(Input.mousePosition);
            }
        }

        private void OnPositionChanged(Vector3 position)
        {
            if (!_dragging)
            {
                ((RectTransform)transform).anchoredPosition = position;
            }
        }

        private void OnImageChanged(Sprite image)
        {
            _image.sprite = image;
        }
    }
}