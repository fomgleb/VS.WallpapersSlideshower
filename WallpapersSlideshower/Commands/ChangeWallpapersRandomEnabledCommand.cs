using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class ChangeWallpapersRandomEnabledCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public ChangeWallpapersRandomEnabledCommand(MainWindowViewModel mainWindowViewModel, WallpaperSlideshow wallpaperSlideshow)
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
                _wallpaperSlideshow.WallpapersSelectionMode = WallpaperSlideshow.Mode.Random;
            else
                _wallpaperSlideshow.WallpapersSelectionMode = WallpaperSlideshow.Mode.OneAfterAnother;
        }
    }
}
