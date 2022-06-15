using System;
using UnityEngine;
using TMPro;

namespace UnityMVVM.UIBind
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_InputField))]
    [DefaultExecutionOrder(ExecutionOrderConfig.UIBindOrder)]
    public class TmpInputFieldBinder : UIBinder
    {
        [SerializeField]
        protected TMP_InputField _inputField;

        protected void Start()
        {
            if (_inputField == null)
            {
                _inputField = GetComponent<TMP_InputField>();
            }

            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnEndEdit(string context)
        {
            if (_viewModel == null)
            {
                throw new NullReferenceException("ViewModel is null");
            }

            if (_viewModel.HasCommand(_reactivePath))
            {
                _viewModel.ExecuteCommand(_reactivePath, new ReactiveCommandInputFieldArgs(context));
            }
            else
            {
                throw new ArgumentException($"Reactive path is not valid: {_reactivePath}");
            }
        }
    }
}
