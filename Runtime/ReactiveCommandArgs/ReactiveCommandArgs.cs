using System;

namespace UnityMVVM
{
    public abstract class ReactiveCommandArgs : EventArgs
    {
        public ReactiveCommandArgType Type { get; protected set; }
    }
}
