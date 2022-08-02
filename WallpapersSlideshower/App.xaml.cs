using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;
using WallpapersSlideshower.Properties;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;

namespace WallpapersSlideshower
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly WallpapersSlideshow _wallpapersSlideshow;
        private readonly MainWindowViewModel _mainWindowViewModel;

        private readonly string _existingWallpapersPath;
        private readonly string _pathToWallpapersFolderPath;
        private readonly string _wallpapersSelectionModePath;
        private readonly string _currentDesktopWallpaperPath;

        public App()
        {
            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "Data");

            _existingWallpapersPath = GeneratePathToData(nameof(WallpapersSlideshow.ExistingWallpapers));
            _pathToWallpapersFolderPath = GeneratePathToData(nameof(WallpapersSlideshow.PathToWallpapersFolder));
            _wallpapersSelectionModePath = GeneratePathToData(nameof(WallpapersSlideshow.WallpapersSelectionMode));
            _currentDesktopWallpaperPath = GeneratePathToData(nameof(WallpapersSlideshow.CurrentDesktopWallpaper));

            var existingWallpapers = SaverLoader.Load<ObservableCollection<Wallpaper>>(_existingWallpapersPath);
            var pathToWallpapersFolder = SaverLoader.Load<string>(_pathToWallpapersFolderPath);
            var wallpapersSelectionMode = SaverLoader.Load<WallpapersSlideshow.Mode>(_wallpapersSelectionModePath);
            var currentDesktopWallpaper = SaverLoader.Load<Wallpaper>(_currentDesktopWallpaperPath);

            if (existingWallpapers == null)
                existingWallpapers = new ObservableCollection<Wallpaper>();
            if (pathToWallpapersFolder == null)
                pathToWallpapersFolder = "";

            _wallpapersSlideshow = new WallpapersSlideshow(existingWallpapers, pathToWallpapersFolder, wallpapersSelectionMode,
                currentDesktopWallpaper);

            var randomIsEnabled = wallpapersSelectionMode == WallpapersSlideshow.Mode.Random;
            var slideshowIsEnabled = Settings.Default.SlideshowIsEnabled;
            var autorunIsEnabled = Settings.Default.AutorunIsEnabled;

            _mainWindowViewModel = new MainWindowViewModel(_wallpapersSlideshow, randomIsEnabled, slideshowIsEnabled, autorunIsEnabled);
        }

        private static string GeneratePathToData(string variableName)
        {
            return System.Windows.Forms.Application.StartupPath + "Data\\" + variableName + ".dat";
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
            SaverLoader.Save(_existingWallpapersPath, _wallpapersSlideshow.ExistingWallpapers);
            SaverLoader.Save(_pathToWallpapersFolderPath, _wallpapersSlideshow.PathToWallpapersFolder);
            SaverLoader.Save(_wallpapersSelectionModePath, _wallpapersSlideshow.WallpapersSelectionMode);
            if (_wallpapersSlideshow.CurrentDesktopWallpaper != null)
                SaverLoader.Save(_currentDesktopWallpaperPath, _wallpapersSlideshow.CurrentDesktopWallpaper);
            Settings.Default.SlideshowIsEnabled = _mainWindowViewModel.SlideshowIsEnabled;
            Settings.Default.AutorunIsEnabled = _mainWindowViewModel.AutorunIsEnabled;
            Settings.Default.Save();
            base.OnExit(e);
        }
    }
}
