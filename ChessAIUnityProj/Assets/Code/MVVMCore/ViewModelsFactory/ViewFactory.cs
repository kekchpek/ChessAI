using UnityEngine;
using Zenject;

namespace MVVMCore.ViewModelsFactory
{
    public class ViewFactory<TView, TViewModel> : IViewFactory<TViewModel> 
        where TView : IViewInitializer<TViewModel>
        where TViewModel : IViewModel
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _viewPrefab;

        public ViewFactory(IInstantiator instantiator, GameObject viewPrefab)
        {
            _instantiator = instantiator;
            _viewPrefab = viewPrefab;
        }
        
        public TViewModel Create()
        {
            var viewModel = _instantiator.Instantiate<TViewModel>();
            var view = _instantiator.InstantiatePrefabForComponent<TView>(_viewPrefab);
            view.SetViewModel(viewModel);
            return viewModel;
        }
    }
}