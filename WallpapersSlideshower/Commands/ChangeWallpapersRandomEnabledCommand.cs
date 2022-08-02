using System;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class ChangeWallpapersRandomEnabledCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly WallpapersSlideshow _wallpaperSlideshow;

        public ChangeWallpapersRandomEnabledCommand(MainWindowViewModel mainWindowViewModel, WallpapersSlideshow wallpaperSlideshow)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _wallpaperSlideshow = wallpaperSlideshow;
        }

        public override bool CanExecute(object? parameter)
        {
            return _mainWindowViewModel.WallpapersViewModels.Count != 0 && _mainWindowViewModel.PathToFolder != null;
        }

        public override void Execute(object? parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter), "Argument can't be null.");
            var enableRandom = (bool)parameter;
            if (enableRandom)
                _wallpaperSlideshow.WallpapersSelectionMode = WallpapersSlideshow.Mode.Random;
            else
                _wallpaperSlideshow.WallpapersSelectionMode = WallpapersSlideshow.Mode.OneAfterAnother;
        }
    }
}
