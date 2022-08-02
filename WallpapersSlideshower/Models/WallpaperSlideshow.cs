using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WallpapersSlideshower.Models
{
    [Serializable]
    public class WallpapersSlideshow
    {
        public event Action? DesktopWallpaperChangedEvent;

        public ObservableCollection<Wallpaper> ExistingWallpapers { get; set; }
        public string PathToWallpapersFolder { get; private set; }
        public Mode WallpapersSelectionMode { get; set; }

        public Wallpaper? CurrentDesktopWallpaper { get; private set; }

        private static readonly string[] IMAGE_EXTENTIONS =
        {
            ".png",
            ".jpg"
        };

        public WallpapersSlideshow(ObservableCollection<Wallpaper> existingWallpapers, string pathToWallpapersFolder,
            Mode wallpapersSelectionMode, Wallpaper currentDesktopWallpaper)
        {
            ExistingWallpapers = existingWallpapers;
            PathToWallpapersFolder = pathToWallpapersFolder;
            WallpapersSelectionMode = wallpapersSelectionMode;
            CurrentDesktopWallpaper = currentDesktopWallpaper;
            DesktopWallpaperChanger.WallpaperChangedEvent += OnWallpaerChanged;
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

        public Task? CurrentSetWallpaperTask { get; private set; }
        public async void ShowNextWallpaper()
        {
            switch (WallpapersSelectionMode)
            {
                case Mode.OneAfterAnother:
                    if (CurrentDesktopWallpaper == null)
                    {
                        CurrentDesktopWallpaper = ExistingWallpapers[0];
                        break;
                    }
                    var indexOfCurrentDesktopWallpaper = ExistingWallpapers.IndexOf(CurrentDesktopWallpaper);
                    if (indexOfCurrentDesktopWallpaper == -1)
                    {
                        CurrentDesktopWallpaper = ExistingWallpapers[0];
                        break;
                    }
                    CurrentDesktopWallpaper = indexOfCurrentDesktopWallpaper + 1 >= ExistingWallpapers.Count ?
                        CurrentDesktopWallpaper = ExistingWallpapers[0] :
                        CurrentDesktopWallpaper = ExistingWallpapers[indexOfCurrentDesktopWallpaper + 1];
                    break;
                case Mode.Random:
                        var allWallpapersWithoutCurrent = ExistingWallpapers.Where(wallpaper => !wallpaper.Equals(CurrentDesktopWallpaper)).ToArray();
                    var random = new Random();
                    CurrentDesktopWallpaper = allWallpapersWithoutCurrent[random.Next(0, allWallpapersWithoutCurrent.Length)];
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(WallpapersSelectionMode), "Enum element not implemented.");
            }

            if (CurrentSetWallpaperTask != null && !CurrentSetWallpaperTask.IsCompleted)
                await CurrentSetWallpaperTask;
            CurrentSetWallpaperTask = DesktopWallpaperChanger.SetWallpaperAsync(new Uri(CurrentDesktopWallpaper.PathToImage), DesktopWallpaperChanger.Style.Span);
        }

        private async void OnWallpaerChanged()
        {
            if (CurrentSetWallpaperTask != null)
                await CurrentSetWallpaperTask;
            DesktopWallpaperChangedEvent?.Invoke();
        }

        public enum Mode
        {
            OneAfterAnother,
            Random,
        }
    }
}
