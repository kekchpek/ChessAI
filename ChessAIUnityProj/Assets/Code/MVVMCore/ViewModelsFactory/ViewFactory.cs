using System;
using UnityEngine;
using Zenject;

namespace MVVMCore.ViewModelsFactory
{
    public class ViewFactory<TView, TViewModel, TViewModelImpl> : IViewFactory<TViewModel>
        where TView : IViewInitializer<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _viewPrefab;
        private readonly Transform _container;

        public ViewFactory(IInstantiator instantiator, GameObject viewPrefab, Transform container)
        {
            _instantiator = instantiator;
            _viewPrefab = viewPrefab;
            _container = container;
        }

        public TViewModel Create()
        {
            var viewModel = _instantiator.Instantiate<TViewModelImpl>(Array.Empty<object>());
            if (viewModel is IInitializable initializable)
            {
                initializable.Initialize();
            }
            var view = _instantiator.InstantiatePrefabForComponent<TView>(_viewPrefab, _container);
            view.SetViewModel(viewModel);
            return viewModel;
        }
    }

    public class ViewFactory<TView, TViewModel, TViewModelImpl, TArg> : IViewFactory<TViewModel, TArg>
        where TView : IViewInitializer<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _viewPrefab;
        private readonly Transform _container;

        public ViewFactory(IInstantiator instantiator, GameObject viewPrefab, Transform container)
        {
            _instantiator = instantiator;
            _viewPrefab = viewPrefab;
            _container = container;
        }

        public TViewModel Create(TArg arg)
        {
            var viewModel = _instantiator.Instantiate<TViewModelImpl>(new object[] {arg});
            if (viewModel is IInitializable initializable)
            {
                initializable.Initialize();
            }
            var view = _instantiator.InstantiatePrefabForComponent<TView>(_viewPrefab, _container);
            view.SetViewModel(viewModel);
            return viewModel;
        }
    }

    public class ViewFactory<TView, TViewModel, TViewModelImpl, TArg1, TArg2> : IViewFactory<TViewModel, TArg1, TArg2>
        where TView : IViewInitializer<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _viewPrefab;
        private readonly Transform _container;

        public ViewFactory(IInstantiator instantiator, GameObject viewPrefab, Transform container)
        {
            _instantiator = instantiator;
            _viewPrefab = viewPrefab;
            _container = container;
        }

        public TViewModel Create(TArg1 arg1, TArg2 arg2)
        {
            var viewModel = _instantiator.Instantiate<TViewModelImpl>(new object[] {arg1, arg2});
            if (viewModel is IInitializable initializable)
            {
                initializable.Initialize();
            }
            var view = _instantiator.InstantiatePrefabForComponent<TView>(_viewPrefab, _container);
            view.SetViewModel(viewModel);
            return viewModel;
        }
    }

    public class ViewFactory<TView, TViewModel, TViewModelImpl, TArg1, TArg2, TArg3> : IViewFactory<TViewModel, TArg1, TArg2, TArg3>
        where TView : IViewInitializer<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _viewPrefab;
        private readonly Transform _container;

        public ViewFactory(IInstantiator instantiator, GameObject viewPrefab, Transform container)
        {
            _instantiator = instantiator;
            _viewPrefab = viewPrefab;
            _container = container;
        }

        public TViewModel Create(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var viewModel = _instantiator.Instantiate<TViewModelImpl>(new object[] {arg1, arg2, arg3});
            if (viewModel is IInitializable initializable)
            {
                initializable.Initialize();
            }
            var view = _instantiator.InstantiatePrefabForComponent<TView>(_viewPrefab, _container);
            view.SetViewModel(viewModel);
            return viewModel;
        }
    }

    public class ViewFactory<TView, TViewModel, TViewModelImpl, TArg1, TArg2, TArg3, TArg4> : IViewFactory<TViewModel, TArg1, TArg2, TArg3, TArg4>
        where TView : IViewInitializer<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _viewPrefab;
        private readonly Transform _container;

        public ViewFactory(IInstantiator instantiator, GameObject viewPrefab, Transform container)
        {
            _instantiator = instantiator;
            _viewPrefab = viewPrefab;
            _container = container;
        }

        public TViewModel Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var viewModel = _instantiator.Instantiate<TViewModelImpl>(new object[] {arg1, arg2, arg3, arg4});
            if (viewModel is IInitializable initializable)
            {
                initializable.Initialize();
            }
            var view = _instantiator.InstantiatePrefabForComponent<TView>(_viewPrefab, _container);
            view.SetViewModel(viewModel);
            return viewModel;
        }
    }
}