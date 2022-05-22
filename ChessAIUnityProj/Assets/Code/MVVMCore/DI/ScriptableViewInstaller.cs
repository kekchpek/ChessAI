using MVVMCore.ViewModelsFactory;
using UnityEngine;
using Zenject;

namespace MVVMCore.DI
{
    public abstract class ScriptableViewInstaller : ScriptableObjectInstaller
    {

        private DiContainer _viewsContainer;
        
        /// <summary>
        /// Installs <see cref="IViewFactory{TViewModel}"/> for specified View-ViewModel pair.
        /// </summary>
        /// <param name="viewPrefab">View prefab. It will be instantiated on creation. It should contains <see cref="TView"/> component inside.</param>
        /// <typeparam name="TView">The type of a view</typeparam>
        /// <typeparam name="TViewModel">The type of a view model.</typeparam>
        /// <typeparam name="TViewModelImpl">The type, that implements a view model.</typeparam>
        protected void InstallFactoryFor<TView, TViewModel, TViewModelImpl>(GameObject viewPrefab)
        where TView : ViewBehaviour<TViewModel>
        where TViewModel : IViewModel
        where TViewModelImpl : class, TViewModel
        {
            _viewsContainer.Bind<TViewModel>().To<TViewModelImpl>().AsTransient();
            _viewsContainer.Bind<IViewFactory<TViewModel>>().To<ViewFactory<TView, TViewModel>>().AsSingle().WithArguments(viewPrefab);
            Container.Bind<IViewFactory<TViewModel>>().FromSubContainerResolve();
        }

        public override void InstallBindings()
        {
            base.InstallBindings();
            _viewsContainer = Container.CreateSubContainer();
        }
    }
}