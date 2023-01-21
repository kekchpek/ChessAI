using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityMVVM;
using Zenject;

namespace KChessUnity.MVVM.Views.MovesDisplayer
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
            ViewModel.HighlightedPositions.Bind(OnHighlightedPositionChanged);
        }

        private void OnHighlightedPositionChanged(IEnumerable<Vector3> highlightedPositions)
        {
            var array = highlightedPositions == null ? Array.Empty<Vector3>() : highlightedPositions.ToArray();
            foreach (var marker in _markers)
            {
                marker.SetActive(false);
            }

            while (_markers.Count < array.Length)
            {
                AddMarkers(MarkersAddition);
            }

            for (var i = 0; i < array.Length; i++)
            {
                ((RectTransform)_markers[i].transform).anchoredPosition = array[i];
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