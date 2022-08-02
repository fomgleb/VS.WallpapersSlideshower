using System.Windows;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class OnWindowStateChangedCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public OnWindowStateChangedCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (_mainWindowViewModel.WindowState == WindowState.Minimized)
            {
                _mainWindowViewModel.WindowVisibility = false;
                _mainWindowViewModel.WindowState = WindowState.Normal;
            }
        }
    }
}
