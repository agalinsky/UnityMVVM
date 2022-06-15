namespace UnityMVVM
{
    public interface IReactiveProperty
    {
        /// <summary>
        /// Value type of reactive property and value type of view component can be not the same.
        /// </summary>
        void Subscribe<ViewValueType>(ReactiveMemberChangedHandler<ViewValueType> updateViewHandlingMethod);
        /// <summary>
        /// Force to invoke OnValueChanged event for manual update.
        /// </summary>
        void ForceInvoke();
    }

    /// <summary>
    /// T must be Float, Int, Bool, String
    /// </summary>
    public interface IReactiveProperty<T> : IReactiveProperty
    {
        T Value { get; set; }
    }

    // Remarks:
    //  May be it is better to rename 'IReactiveCommand' to 'IDelegateCommand'
    //  because in fact it is general delegate realization of pattern 'Command'.
    //
    public interface IReactiveCommand
    {
        bool CanExecute();
        void Execute(ReactiveCommandArgs args);
    }
}
