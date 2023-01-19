using KChessUnity.ViewModels.Piece;
using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;

namespace KChessUnity.Views
{
    public class PieceView : ViewBehaviour<IPieceViewModel>
    {
        [SerializeField] private Image _image;

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

            ViewModel.Disposed += OnDisposed;
        }

        private void OnDisposed()
        {
            Destroy(gameObject);
        }

        private void OnPositionChanged(Vector3 position)
        {
            var transformCatch = transform;
            var maxIndex = transformCatch.childCount - 1;
            if (transformCatch.GetSiblingIndex() != maxIndex)
            {
                transformCatch.SetSiblingIndex(maxIndex);
            }
            transformCatch.position = position;
        }

        private void OnImageChanged(Sprite image)
        {
            _image.sprite = image;
        }
    }
}