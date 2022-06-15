using System;

namespace UnityMVVM
{
    /// <summary>
    /// Reaction of view model from view side to execute delegate based command when Unity UI event was occured, e.g. button clicked, input field editing finished, etc.
    /// </summary>
    public class ReactiveCommand : IReactiveCommand
    {
        protected readonly string _path;

        protected Action<object> _onExecute;
        protected NonGenericPredicate _canExecute;

        public ReactiveCommand(string path, IViewModel owner, Action<object> onExecute, NonGenericPredicate canExecute=null)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Reactive path is null or empty");
            }

            if (owner == null)
            {
                throw new ArgumentNullException("ReactiveCommand must have owner");
            }

            if (onExecute == null)
            {
                throw new ArgumentNullException("OnExecute action delegate is null");
            }

            _onExecute = onExecute;
            _canExecute = canExecute;

            _path = path;

            owner.AddCommand(path, this);
        }

        public bool CanExecute()
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute.Invoke();
        }

        public void Execute(ReactiveCommandArgs args)
        {
            switch (args.Type)
            {
                case ReactiveCommandArgType.Button:
                    _onExecute(null);
                    break;
                case ReactiveCommandArgType.InputField:
                    _onExecute((args as ReactiveCommandInputFieldArgs).Context);
                    break;
            }            
        }
    }
}
