using MVVMCore.ViewModelsFactory;
using UnityEngine;
using Zenject;

namespace MVVMCore.DI
{
    public class MonoMvvmInstaller : MonoInstaller
    {
        private DiContainer _viewsContainer;

        /// <summary>
        /// Installs <see cref="IViewFactory{TViewModel}"/> for specified View-ViewModel pair.
        /// </summary>
        /// <param name="viewPrefab">View prefab. It will be instantiated on creation. It should contains <see cref="TView"/> component inside.</param>
        /// <param name="container">The container for created objects.</param>
        /// <typeparam name="TView">The type of a view</typeparam>
        /// <typeparam name="TViewModel">The type of a view model.</typeparam>
        /// <typeparam name="TViewModelImpl">The type, that implements a view model.</typeparam>
        protected void InstallFactoryFor<TView, TViewModel, TViewModelImpl>(GameObject viewPrefab, Transform container)
        where TView : ViewBehaviour<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
        {
            _viewsContainer.Bind<IViewFactory<TViewModel>>().To<ViewFactory<TView, TViewModel, TViewModelImpl>>()
                .AsSingle()
                .WithArgumentsExplicit(new []
                {
                    new TypeValuePair(typeof(GameObject), viewPrefab),
                    new TypeValuePair(typeof(Transform), container)
                });
            Container.Bind<IViewFactory<TViewModel>>().FromSubContainerResolve().ByInstance(_viewsContainer).AsSingle();
        }

        /// <summary>
        /// Installs <see cref="IViewFactory{TViewModel}"/> for specified View-ViewModel pair.
        /// </summary>
        /// <param name="viewPrefab">View prefab. It will be instantiated on creation. It should contains <see cref="TView"/> component inside.</param>
        /// <param name="container">The container for created objects.</param>
        /// <typeparam name="TView">The type of a view</typeparam>
        /// <typeparam name="TViewModel">The type of a view model.</typeparam>
        /// <typeparam name="TViewModelImpl">The type, that implements a view model.</typeparam>
        /// <typeparam name="TArg1">A type for an argument for object creation.</typeparam>
        protected void InstallFactoryFor<TView, TViewModel, TViewModelImpl, TArg1>(GameObject viewPrefab, Transform container)
            where TView : ViewBehaviour<TViewModel>
            where TViewModel : IViewModel
            where TViewModelImpl : class, TViewModel
        {
            _viewsContainer.Bind<IViewFactory<TViewModel, TArg1>>().To<ViewFactory<TView, TViewModel, TViewModelImpl, TArg1>>()
                .AsSingle()
                .WithArgumentsExplicit(new []
                {
                    new TypeValuePair(typeof(GameObject), viewPrefab),
                    new TypeValuePair(typeof(Transform), container)
                });
            Container.Bind<IViewFactory<TViewModel, TArg1>>().FromSubContainerResolve().ByInstance(_viewsContainer).AsSingle();
        }

        /// <summary>
        /// Installs <see cref="IViewFactory{TViewModel}"/> for specified View-ViewModel pair.
        /// </summary>
        /// <param name="viewPrefab">View prefab. It will be instantiated on creation. It should contains <see cref="TView"/> component inside.</param>
        /// <param name="container">The container for created objects.</param>
        /// <typeparam name="TView">The type of a view</typeparam>
        /// <typeparam name="TViewModel">The type of a view model.</typeparam>
        /// <typeparam name="TViewModelImpl">The type, that implements a view model.</typeparam>
        /// <typeparam name="TArg1">A type for an argument for object creation.</typeparam>
        /// <typeparam name="TArg2">A type for an argument for object creation.</typeparam>
        protected void InstallFactoryFor<TView, TViewModel, TViewModelImpl, TArg1, TArg2>(GameObject viewPrefab, Transform container)
            where TView : ViewBehaviour<TViewModel>
            where TViewModel : IViewModel
            where TViewModelImpl : class, TViewModel
        {
            _viewsContainer.Bind<IViewFactory<TViewModel, TArg1, TArg2>>().To<ViewFactory<TView, TViewModel, TViewModelImpl, TArg1, TArg2>>()
                .AsSingle()
                .WithArgumentsExplicit(new []
                {
                    new TypeValuePair(typeof(GameObject), viewPrefab),
                    new TypeValuePair(typeof(Transform), container)
                });
            Container.Bind<IViewFactory<TViewModel, TArg1, TArg2>>().FromSubContainerResolve().ByInstance(_viewsContainer).AsSingle();
        }

        /// <summary>
        /// Installs <see cref="IViewFactory{TViewModel}"/> for specified View-ViewModel pair.
        /// </summary>
        /// <param name="viewPrefab">View prefab. It will be instantiated on creation. It should contains <see cref="TView"/> component inside.</param>
        /// <param name="container">The container for created objects.</param>
        /// <typeparam name="TView">The type of a view</typeparam>
        /// <typeparam name="TViewModel">The type of a view model.</typeparam>
        /// <typeparam name="TViewModelImpl">The type, that implements a view model.</typeparam>
        /// <typeparam name="TArg1">A type for an argument for object creation.</typeparam>
        /// <typeparam name="TArg2">A type for an argument for object creation.</typeparam>
        /// <typeparam name="TArg3">A type for an argument for object creation.</typeparam>
        protected void InstallFactoryFor<TView, TViewModel, TViewModelImpl, TArg1, TArg2, TArg3>(GameObject viewPrefab, Transform container)
            where TView : ViewBehaviour<TViewModel>
            where TViewModel : IViewModel
            where TViewModelImpl : class, TViewModel
        {
            _viewsContainer.Bind<IViewFactory<TViewModel, TArg1, TArg2, TArg3>>().To<ViewFactory<TView, TViewModel, TViewModelImpl, TArg1, TArg2, TArg3>>()
                .AsSingle()
                .WithArgumentsExplicit(new []
                {
                    new TypeValuePair(typeof(GameObject), viewPrefab),
                    new TypeValuePair(typeof(Transform), container)
                });
            Container.Bind<IViewFactory<TViewModel, TArg1, TArg2, TArg3>>().FromSubContainerResolve().ByInstance(_viewsContainer).AsSingle();
        }

        /// <summary>
        /// Installs <see cref="IViewFactory{TViewModel}"/> for specified View-ViewModel pair.
        /// </summary>
        /// <param name="viewPrefab">View prefab. It will be instantiated on creation. It should contains <see cref="TView"/> component inside.</param>
        /// <param name="container">The container for created objects.</param>
        /// <typeparam name="TView">The type of a view</typeparam>
        /// <typeparam name="TViewModel">The type of a view model.</typeparam>
        /// <typeparam name="TViewModelImpl">The type, that implements a view model.</typeparam>
        /// <typeparam name="TArg1">A type for an argument for object creation.</typeparam>
        /// <typeparam name="TArg2">A type for an argument for object creation.</typeparam>
        /// <typeparam name="TArg3">A type for an argument for object creation.</typeparam>
        /// <typeparam name="TArg4">A type for an argument for object creation.</typeparam>
        protected void InstallFactoryFor<TView, TViewModel, TViewModelImpl, TArg1, TArg2, TArg3, TArg4>(GameObject viewPrefab, Transform container)
            where TView : ViewBehaviour<TViewModel>
            where TViewModel : IViewModel
            where TViewModelImpl : class, TViewModel
        {
            _viewsContainer.Bind<IViewFactory<TViewModel, TArg1, TArg2, TArg3, TArg4>>().To<ViewFactory<TView, TViewModel, TViewModelImpl, TArg1, TArg2, TArg3, TArg4>>()
                .AsSingle()
                .WithArgumentsExplicit(new []
                {
                    new TypeValuePair(typeof(GameObject), viewPrefab),
                    new TypeValuePair(typeof(Transform), container)
                });
            Container.Bind<IViewFactory<TViewModel, TArg1, TArg2, TArg3, TArg4>>().FromSubContainerResolve().ByInstance(_viewsContainer).AsSingle();
        }

        public override void InstallBindings()
        {
            base.InstallBindings();
            _viewsContainer = Container.CreateSubContainer();
        }
    }
}