using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace WallpapersSlideshower.Models
{
    [Serializable]
    public class WallpaperSlideshow
    {
        public ObservableCollection<Wallpaper> ExistingWallpapers { get; set; }
        public string? PathToWallpapersFolder { get; private set; }
        public Mode WallpapersSelectionMode { get; set; } = Mode.OneAfterAnother;

        private Wallpaper? _currentDesktopWallpaper;

        private static readonly string[] IMAGE_EXTENTIONS =
        {
            ".png",
            ".jpg"
        };

        public WallpaperSlideshow()
        {
            ExistingWallpapers = new ObservableCollection<Wallpaper>();
        }

        public void GetWallpapersFromFolder(string pathToFolder, SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(pathToFolder))
                throw new ArgumentNullException(nameof(pathToFolder), "Argument can't be null or white space.");
            PathToWallpapersFolder = pathToFolder;

            var pathsToImages = Directory.EnumerateFiles(pathToFolder, "*.*", searchOption).Where(pathToFile =>
            {
                foreach (var imageExtension in IMAGE_EXTENTIONS)
                    if (pathToFile.EndsWith(imageExtension))
                        return true;
                return false;
            }).ToList();
            ExistingWallpapers.Clear();
            foreach (var pathToImage in pathsToImages)
                ExistingWallpapers.Add(new Wallpaper(pathToImage));
        }

        public void ShowNextWallpaper()
        {
            switch (WallpapersSelectionMode)
            {
                case Mode.OneAfterAnother:
                    if (_currentDesktopWallpaper == null)
                    {
                        _currentDesktopWallpaper = ExistingWallpapers[0];
                        return;
                    }
                    var indexOfCurrentDesktopWallpaper = ExistingWallpapers.IndexOf(_currentDesktopWallpaper);
                    _currentDesktopWallpaper = indexOfCurrentDesktopWallpaper + 1 >= ExistingWallpapers.Count ?
                        _currentDesktopWallpaper = ExistingWallpapers[0] :
                        _currentDesktopWallpaper = ExistingWallpapers[indexOfCurrentDesktopWallpaper + 1];
                    break;
                case Mode.Random:
                    var allWallpapersWithoutCurrent = ExistingWallpapers.Where(wallpaper => wallpaper != _currentDesktopWallpaper).ToArray();
                    var random = new Random();
                    _currentDesktopWallpaper = allWallpapersWithoutCurrent[random.Next(0, allWallpapersWithoutCurrent.Length)];
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(WallpapersSelectionMode), "Enum element not implemented.");
            }

            DesktopWallpaperChanger.SetWallpaperAsync(new Uri(_currentDesktopWallpaper.PathToImage), DesktopWallpaperChanger.Style.Span);
        }

        public enum Mode
        {
            OneAfterAnother,
            Random,
        }
    }
}
