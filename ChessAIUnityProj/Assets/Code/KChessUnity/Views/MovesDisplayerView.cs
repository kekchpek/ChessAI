using System.Collections.Generic;
using KChessUnity.ViewModels.MovesDisplayer;
using MVVMCore;
using UnityEngine;
using Zenject;

namespace KChessUnity.Views
{
    public class MovesDisplayerView : ViewBehaviour<IMovesDisplayerViewModel>
    {

        private const int MarkersAddition = 10;
        
        [SerializeField] private GameObject _markerPrefab;
        [SerializeField] private List<GameObject> _markers;

        private IInstantiator _instantiator;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SubscribeForPropertyChange<Vector3[]>(nameof(ViewModel.HighlightedPositions), OnHighlightedPositionChanged);
        }

        private void OnHighlightedPositionChanged(Vector3[] highlightedPositions)
        {
            foreach (var marker in _markers)
            {
                marker.SetActive(false);
            }

            while (_markers.Count < highlightedPositions.Length)
            {
                AddMarkers(MarkersAddition);
            }

            for (var i = 0; i < highlightedPositions.Length; i++)
            {
                _markers[i].transform.position = highlightedPositions[i];
                _markers[i].SetActive(true);
            }
        }

        private void AddMarkers(int markersCount)
        {
            for (var i = 0; i < markersCount; i++)
            {
                var marker = _instantiator.InstantiatePrefab(_markerPrefab, transform);
                marker.SetActive(false);
                _markers.Add(marker);
            }
        }
    }
}