using System;
using System.Windows;
using System.Windows.Controls;
using WallpapersSlideshower.Models;
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

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var visible = (bool)parameter;
            _mainWindowViewModel.WindowVisibility = visible;
        }
    }
}
