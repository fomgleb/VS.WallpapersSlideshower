using Microsoft.Win32;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;
using System;

namespace WallpapersSlideshower.Commands
{
    public class ChangeSlideshowEnabledCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly WallpapersSlideshow _wallpaperSlideshow;

        public ChangeSlideshowEnabledCommand(MainWindowViewModel mainWindowViewModel, WallpapersSlideshow wallpaperSlideshow)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _wallpaperSlideshow = wallpaperSlideshow;
        }

        public override bool CanExecute(object? parameter)
        {
            var a = _mainWindowViewModel.WallpapersViewModels.Count != 0 &&
                _mainWindowViewModel.PathToFolder != null &&
               (_wallpaperSlideshow.CurrentSetWallpaperTask == null || _wallpaperSlideshow.CurrentSetWallpaperTask.IsCompleted);
            Console.WriteLine(a);
            return a;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter), "Argument can't be null.");
            var enableSlideshow = (bool)parameter;

            if (enableSlideshow)
            {
                SystemEvents.PowerModeChanged += OnPowerModeChanged;
                _wallpaperSlideshow.ShowNextWallpaper();
            }
            else
                SystemEvents.PowerModeChanged -= OnPowerModeChanged;
            _mainWindowViewModel.SlideshowIsEnabled = enableSlideshow;
        }

        private void OnPowerModeChanged(object? sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.Resume) return;
            _wallpaperSlideshow.ShowNextWallpaper();
        }
    }
}
