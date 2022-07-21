using System.Collections.Generic;
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
        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public App()
        {
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
    }
}
