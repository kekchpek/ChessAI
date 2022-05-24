using KChessUnity.ViewModels.Piece;
using MVVMCore;
using UnityEngine;
using UnityEngine.UI;

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
            
            SubscribeForPropertyChange<Vector3>(nameof(ViewModel.Position), OnPositionChanged);
            OnPositionChanged(ViewModel.Position);
            
            SubscribeForPropertyChange<Sprite>(nameof(ViewModel.Image), OnImageChanged);
            OnImageChanged(ViewModel.Image);

            ViewModel.Disposed += OnDisposed;
        }

        private void OnDisposed()
        {
            Destroy(gameObject);
        }

        private void OnPositionChanged(Vector3 position)
        {
            transform.position = position;
        }

        private void OnImageChanged(Sprite image)
        {
            _image.sprite = image;
        }
    }
}