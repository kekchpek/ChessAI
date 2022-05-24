namespace MVVMCore.ViewModelsFactory
{
    public interface IViewFactory<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel Create();
    }
    
    public interface IViewFactory<out TViewModel, in TArg1> where TViewModel : IViewModel
    {
        TViewModel Create(TArg1 arg);
    }
    
    public interface IViewFactory<out TViewModel, in TArg1, in TArg2> where TViewModel : IViewModel
    {
        TViewModel Create(TArg1 arg1, TArg2 arg2);
    }
    
    public interface IViewFactory<out TViewModel, in TArg1, in TArg2, in TArg3> where TViewModel : IViewModel
    {
        TViewModel Create(TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
    
    public interface IViewFactory<out TViewModel, in TArg1, in TArg2, in TArg3, in TArg4> where TViewModel : IViewModel
    {
        TViewModel Create(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    }
}