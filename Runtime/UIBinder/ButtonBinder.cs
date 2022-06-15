using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityMVVM.UIBind
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    [DefaultExecutionOrder(ExecutionOrderConfig.UIBindOrder)]
    public class ButtonBinder : UIBinder
    {
        [SerializeField]
        protected Button _button;

        protected void Start()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            _button.onClick.AddListener(OnButtonClicked);            
        }

        protected virtual void OnButtonClicked()
        {
            if (_viewModel == null)
            {
                throw new NullReferenceException("ViewModel is null");
            }

            if (_viewModel.HasCommand(_reactivePath))
            {
                _viewModel.ExecuteCommand(_reactivePath, new ReactiveCommandButtonArgs());
            }
            else
            {
                throw new ArgumentException($"Reactive path is not valid: {_reactivePath}");
            }
        }
    }
}
