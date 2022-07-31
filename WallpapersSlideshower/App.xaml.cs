using System.Windows;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;
using WallpapersSlideshower.Properties;

namespace WallpapersSlideshower
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string WALLPAPER_SLIDESHOW_FILE_NAME = "WallpaperSlideshow.dat";
        private readonly WallpaperSlideshow _wallpaperSlideshow;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public App()
        {
            var loadedWallpaperSlideshow = SaverLoader.Load<WallpaperSlideshow>(System.Windows.Forms.Application.StartupPath + WALLPAPER_SLIDESHOW_FILE_NAME);
            if (loadedWallpaperSlideshow != null)
                _wallpaperSlideshow = loadedWallpaperSlideshow;
            else
                _wallpaperSlideshow = new WallpaperSlideshow();

            _mainWindowViewModel = new MainWindowViewModel(_wallpaperSlideshow)
            {
                RandomIsEnabled = Settings.Default.RandomIsEnabled,
                SlideshowIsEnabled = Settings.Default.SlideshowIsEnabled,
                AutorunValue = Settings.Default.AutorunValue
            };
            if (_mainWindowViewModel.SlideshowIsEnabled)
                _mainWindowViewModel.ChangeSlideshowEnabledCommand.Execute(true);
            if (_mainWindowViewModel.AutorunValue == true)
                _mainWindowViewModel.WindowVisibility = false;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = _mainWindowViewModel
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SaverLoader.Save(WALLPAPER_SLIDESHOW_FILE_NAME, _wallpaperSlideshow);
            Settings.Default.RandomIsEnabled = _mainWindowViewModel.RandomIsEnabled;
            Settings.Default.SlideshowIsEnabled = _mainWindowViewModel.SlideshowIsEnabled;
            Settings.Default.AutorunValue = _mainWindowViewModel.AutorunValue;
            Settings.Default.Save();
            base.OnExit(e);
        }
    }
}
