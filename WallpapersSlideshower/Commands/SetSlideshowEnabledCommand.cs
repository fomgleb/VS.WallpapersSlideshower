using Microsoft.Win32;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class SetSlideshowEnabledCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public SetSlideshowEnabledCommand(MainWindowViewModel mainWindowViewModel, WallpaperSlideshow wallpaperSlideshow)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _wallpaperSlideshow = wallpaperSlideshow;
        }

        public override bool CanExecute(object parameter)
        {
            return _mainWindowViewModel.WallpapersViewModels.Count != 0 && _mainWindowViewModel.PathToFolder != null;
        }

        public override void Execute(object parameter)
        {
            if ((bool)parameter == true)
            {
                SystemEvents.PowerModeChanged += OnPowerModeChanged;
                _wallpaperSlideshow.SwitchToFirstWallpaper();
                _wallpaperSlideshow.ShowCurrentWallpaper();
            }
            else
                SystemEvents.PowerModeChanged -= OnPowerModeChanged;
            _mainWindowViewModel.SlideshowIsEnabled = (bool)parameter;
        }

        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.Resume) return;
            _wallpaperSlideshow.SwitchToNextWalppaper();
            _wallpaperSlideshow.ShowCurrentWallpaper();
        }
    }
}
