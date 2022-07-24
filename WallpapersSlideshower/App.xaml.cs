using System.Windows;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string WALLPAPER_SLIDESHOW_FILE_NAME = "WallpaperSlideshow.dat";
        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public App()
        {
            var loadedWallpaperSlideshow = SaverLoader.Load<WallpaperSlideshow>(WALLPAPER_SLIDESHOW_FILE_NAME);
            if (loadedWallpaperSlideshow != null)
                _wallpaperSlideshow = loadedWallpaperSlideshow;
            else
                _wallpaperSlideshow = new WallpaperSlideshow();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_wallpaperSlideshow)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SaverLoader.Save(WALLPAPER_SLIDESHOW_FILE_NAME, _wallpaperSlideshow);
            base.OnExit(e);
        }
    }
}
