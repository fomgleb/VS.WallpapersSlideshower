using Microsoft.Win32;
using System;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class ChangeProgramAutorunValueCommand : CommandBase
    {
        private const string programName = "WallpapersSlideshower";
        private MainWindowViewModel _mainWindowViewModel;

        public ChangeProgramAutorunValueCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var autorun = (bool)parameter;
            string executablePath = System.Windows.Forms.Application.ExecutablePath;
            var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    key.SetValue(programName, executablePath);
                else
                    key.DeleteValue(programName);

                _mainWindowViewModel.AutorunValue = autorun;
                key.Close();
            }
            catch { }
        }
    }
}
