using UnityMVVM;

namespace Samples.UI
{
    public class SampleUIViewModel : ViewModelBase
    {
        // How to create paths to reactive members for ui binding:
        [DropdownItem]
        public const string ReactiveNameCounterLabel = "Counter:CountLabel";
        [DropdownItem]
        public const string ReactiveNameCounterIncrease = "Counter:IncreaseCount";
        [DropdownItem]
        public const string ReactiveNameChangeTitle = "SampleUI:ChangeTitle";
        [DropdownItem]
        public const string ReactiveNameTitleLabel = "SampleUI:TitleLabel";

        private IReactiveProperty<int> _Count;
        private IReactiveProperty<string> _Title;
        private IReactiveCommand _CommandIncrease;
        private IReactiveCommand _CommandChangeTitle;

        private void Awake()
        {
            _Count = new ReactiveProperty<int>(0, ReactiveNameCounterLabel, this);
            _CommandIncrease = new ReactiveCommand(ReactiveNameCounterIncrease, this, IncreaseCounter, null);
            _Title = new ReactiveProperty<string>("Start Title!", ReactiveNameTitleLabel, this);
            _CommandChangeTitle = new ReactiveCommand(ReactiveNameChangeTitle, this, OnChangeTitle, null);
        }

        private void Start()
        {
            base.Start();
        }

        private void IncreaseCounter(object arg)
        {
            _Count.Value = ++_Count.Value;
        }

        private void OnChangeTitle(object arg)
        {
            string nextTitle = arg as string;

            if (nextTitle == null)
            {
                throw new System.InvalidCastException("Invalid argument for OnChangeTitle.");
            }

            _Title.Value = nextTitle;
        }
    }
}

