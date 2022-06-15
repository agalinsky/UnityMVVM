namespace UnityMVVM
{
    public class ReactiveCommandInputFieldArgs : ReactiveCommandArgs
    {
        public string Context { get; protected set; }

        public ReactiveCommandInputFieldArgs(string context)
        {
            Context = context;
            Type = ReactiveCommandArgType.InputField;
        }
    }
}
