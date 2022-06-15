namespace UnityMVVM
{
    /// <summary>
    /// Firstly ViewModels have to initialize its reactive properties and after that UI components can bind to them.
    /// </summary>
    public static class ExecutionOrderConfig
    {
        public const int ViewModelOrder = 0;
        public const int UIBindOrder = 1;
    }
}
