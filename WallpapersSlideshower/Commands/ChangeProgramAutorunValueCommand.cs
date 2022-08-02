using System;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class ChangeProgramAutorunValueCommand : CommandBase
    {
        private const string programName = "WallpapersSlideshower";
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ChangeProgramAutorunValueCommand(MainWindowViewModel mainWindowViewModel)
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
            var enableAutorun = (bool)parameter;
            AutorunChanger.Set(enableAutorun, programName);
            _mainWindowViewModel.AutorunIsEnabled = enableAutorun;
        }
    }
}
