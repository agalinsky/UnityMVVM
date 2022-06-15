using UnityEngine;

namespace UnityMVVM.UIBind
{
    [DefaultExecutionOrder(ExecutionOrderConfig.UIBindOrder)]
    public abstract class UIBinder : MonoBehaviour
    {        
        [DropdownSelector]
        [SerializeField]
        protected string _reactivePath;
        
        [ReadOnly]
        [SerializeField]
        protected ViewModelBase _viewModel;

        protected void Awake()
        {
            if (string.IsNullOrEmpty(_reactivePath))
            {
                throw new System.Exception("Reactive path is not assigned");
            }
            
            _viewModel = GetComponentInParent<ViewModelBase>();
        }
    }
}
