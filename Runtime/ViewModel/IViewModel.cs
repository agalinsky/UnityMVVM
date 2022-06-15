namespace UnityMVVM
{
    /// <summary>
    /// In general it recommends to use abstract mono-realiazation of ViewModel.
    /// But in case of need there is option to inherit own ViewModel using interface.
    /// </summary>
    public interface IViewModel
    {
        bool HasProperty(string reactivePath);
        bool HasCommand(string reactivePath);
        void AddProperty(string reactivePath, IReactiveProperty property);
        void AddCommand(string reactivePath, IReactiveCommand command);
        void ExecuteCommand(string reactivePath, ReactiveCommandArgs args);
        void BindToProperty<ViewValueType>(string reactivePath, ReactiveMemberChangedHandler<ViewValueType> onValueChanged);
    }
}
