using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpapersSlideshower.BusinessLogic.Model;

namespace WallpapersSlideshower.BusinessLogic.Controller
{
    public class WallpapersController
    {
        private static readonly string[] IMAGE_EXTENTIONS =
        {
            ".png",
            ".jpg"
        };

        private string _pathToWallpapersFolder;
        private Wallpaper[] _wallpapers;

        public Wallpaper[] Wallpapers => _wallpapers;

        public WallpapersController(string pathToWallpapersFolder, SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(pathToWallpapersFolder))
                throw new ArgumentNullException(nameof(pathToWallpapersFolder), "Argument can't be null or white space.");
            _pathToWallpapersFolder = pathToWallpapersFolder;

            var pathsToImages = Directory.EnumerateFiles(pathToWallpapersFolder, "*.*", searchOption).Where(pathToFile =>
            {
                foreach (var imageExtension in IMAGE_EXTENTIONS)
                    if (pathToFile.EndsWith(imageExtension))
                        return true;
                return false;
            }).ToArray();
            _wallpapers = new Wallpaper[pathsToImages.Length];
            for (int i = 0; i < _wallpapers.Length; i++)
                _wallpapers[i] = new Wallpaper(pathsToImages[i]);


        }
    }
}
