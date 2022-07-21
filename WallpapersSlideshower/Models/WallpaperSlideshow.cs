using DesktopWallpaper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace WallpapersSlideshower.Models
{
    public class WallpaperSlideshow
    {
        public ObservableCollection<Wallpaper> Wallpapers { get; set; }
        public string PathToWallpapersFolder { get; private set; }
        private int _currentWallpaperIndex;

        private static readonly string[] IMAGE_EXTENTIONS =
        {
            ".png",
            ".jpg"
        };

        public WallpaperSlideshow() 
        {
            Wallpapers = new ObservableCollection<Wallpaper>();
        }

        public void SelectWallpapersFromFolder(string pathToFolder, SearchOption searchOption)
        {
            var absolutePathToFolder = pathToFolder;

            if (string.IsNullOrWhiteSpace(absolutePathToFolder))
                throw new ArgumentNullException(nameof(absolutePathToFolder), "Argument can't be null or white space.");
            PathToWallpapersFolder = pathToFolder;

            var pathsToImages = Directory.EnumerateFiles(absolutePathToFolder, "*.*", searchOption).Where(pathToFile =>
            {
                foreach (var imageExtension in IMAGE_EXTENTIONS)
                    if (pathToFile.EndsWith(imageExtension))
                        return true;
                return false;
            }).ToList();
            foreach (var pathToImage in pathsToImages)
                Wallpapers.Add(new Wallpaper(pathToImage));

            _currentWallpaperIndex = 0;
        }

        public void ShowCurrentWallpaper()
        {
            DesktopWallpaperChanger.Set(new Uri(Wallpapers[_currentWallpaperIndex].PathToImage), DesktopWallpaperChanger.Style.Stretched);
        }

        public void SwitchToNextWalppaper()
        {
            _currentWallpaperIndex++;
            if (_currentWallpaperIndex >= Wallpapers.Count)
                _currentWallpaperIndex = 0;
        }
    }
}
