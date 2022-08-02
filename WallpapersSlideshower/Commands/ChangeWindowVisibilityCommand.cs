using System;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class ChangeWindowVisibilityCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ChangeWindowVisibilityCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter), "Argument can't be null.");
            var enableVisibility = (bool)parameter;
            _mainWindowViewModel.WindowVisibility = enableVisibility;
        }
    }
}
