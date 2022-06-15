using UnityEngine;
using TMPro;

namespace UnityMVVM.UIBind
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]    
    [DefaultExecutionOrder(ExecutionOrderConfig.UIBindOrder)]
    public class TextMeshProBinder : UIBinder
    {
        [SerializeField]
        protected TMP_Text _tmpText;

        protected void Awake()
        {
            if (_tmpText == null)
            {
                _tmpText = GetComponent<TMP_Text>();
            }

            base.Awake();
            _viewModel.BindToProperty<string>(_reactivePath, SetText);
        }

        protected void SetText(string txt)
        {
            _tmpText.text = txt;
        }
    }
}
