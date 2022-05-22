namespace MVVMCore.ViewModelsFactory
{
    public interface IViewFactory<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel Create();
    }
}