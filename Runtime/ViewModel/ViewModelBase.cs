using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR && DEBUG
using UnityMVVM.Editor;
#endif

namespace UnityMVVM
{
    //
    // Remarks:
    //   In general there are two ways of design of view model. First as pure c-sharp classes that can be totally unbound from unity scene but not so convenient in editor use.
    //   Second way as monobehaviour so there is option to customize inspector editor and covenient usage in scene hierarchy.
    //   In both cases main point is that view model has to know nothing about view components.
    //
    // Summary:
    //   
    /// <summary>
    /// ViewModel should be as lean as possible just data with reactive properties and commands of next types: Float, Integer, Bool, String.
    /// </summary>
    [DefaultExecutionOrder(ExecutionOrderConfig.ViewModelOrder)]
    public abstract class ViewModelBase : MonoBehaviour, IViewModel, IDisposable

#if UNITY_EDITOR && DEBUG
        , ISerializationCallbackReceiver
#endif

    {
        protected readonly Dictionary<string, IReactiveProperty> _propertiesTable = new Dictionary<string, IReactiveProperty>();
        protected readonly Dictionary<string, IReactiveCommand> _commandsTable = new Dictionary<string, IReactiveCommand>();

#if UNITY_EDITOR && DEBUG
        [SerializeField]
        protected List<SerializableReactiveProperty> _serializedProperties = new List<SerializableReactiveProperty>();

        public void OnBeforeSerialize()
        {
            _serializedProperties.Clear();
            var Fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in Fields)
            {
                if (typeof(IReactiveProperty).IsAssignableFrom(field.FieldType))
                {
                    _serializedProperties.Add(new SerializableReactiveProperty(field.Name, nameof(IReactiveProperty)));
                }
                else if (typeof(IReactiveCommand).IsAssignableFrom(field.FieldType))
                {
                    _serializedProperties.Add(new SerializableReactiveProperty(field.Name, nameof(IReactiveCommand)));
                }
            }
        }

        public void OnAfterDeserialize() { }
#endif

        protected void Start()
        {
            ForceUpdateProperties();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public bool HasProperty(string reactivePath)
        {
            return _propertiesTable.ContainsKey(reactivePath);
        }

        public bool HasCommand(string reactivePath)
        {
            return _commandsTable.ContainsKey(reactivePath);
        }

        public void AddProperty(string path, IReactiveProperty property)
        {
            _propertiesTable.Add(path, property);
        }

        public void AddCommand(string path, IReactiveCommand command)
        {
            _commandsTable.Add(path, command);
        }

        public void ExecuteCommand(string reactivePath, ReactiveCommandArgs args)
        {
            if (HasCommand(reactivePath))
            {
                var command = _commandsTable[reactivePath];
                if (command.CanExecute())
                {
                    command.Execute(args);
                }
            }
        }

        public void BindToProperty<ViewValueType>(string reactivePath, ReactiveMemberChangedHandler<ViewValueType> onValueChanged)
        {
            GetProperty(reactivePath).Subscribe(onValueChanged);
        }

        protected IReactiveProperty GetProperty(string reactivePath)
        {
            if (_propertiesTable.ContainsKey(reactivePath))
            {
                return _propertiesTable[reactivePath];
            }

            throw new KeyNotFoundException($"ReactiveProperty was not found in dictionary by key: {reactivePath}");
        }

        protected void ForceUpdateProperties()
        {
            foreach (var propertyPair in _propertiesTable)
            {
                propertyPair.Value.ForceInvoke();
            }
        }
    }
}
