using System;

namespace UnityMVVM
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        protected T _value;
        protected readonly string _path;

        protected event ReactiveMemberChangedHandler<T> OnValueChanged;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value.Equals(value) == false)
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }

        public ReactiveProperty(T defaultValue, string path, IViewModel owner)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (owner == null)
            {
                throw new ArgumentNullException("ReactiveProperty must have owner");
            }

            _path = path;
            _value = defaultValue;
            owner.AddProperty(path, this);
        }

        public void Subscribe<ViewValueType>(ReactiveMemberChangedHandler<ViewValueType> onValueChanged)
        {
            OnValueChanged += (value) => {
                ViewValueType convertedValue;
                try
                {
                    convertedValue = (ViewValueType)Convert.ChangeType(value, typeof(ViewValueType));
                }
                catch (Exception e)
                {
                    throw new InvalidCastException($"Failed to cast '{typeof(T)}' to {typeof(ViewValueType)}. Message: {e.Message} {e.StackTrace}");
                }                
                onValueChanged?.Invoke(convertedValue);
            };
        }

        public void ForceInvoke()
        {
            OnValueChanged?.Invoke(_value);
        }
    }
}
