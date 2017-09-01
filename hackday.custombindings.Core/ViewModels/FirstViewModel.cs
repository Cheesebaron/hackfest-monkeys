using MvvmCross.Core.ViewModels;

namespace hackday.custombindings.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        private int _currentIndex;
        public int CurrentIndex 
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        private MvxCommand<int> _changeIndexCommand;

        public MvxCommand<int> ChangeIndexCommand =>
            _changeIndexCommand = _changeIndexCommand ?? 
                new MvxCommand<int>(DoChangeIndexCommand);

        private void DoChangeIndexCommand(int index)
        {
            CurrentIndex = index - 1;
        }
    }
}
