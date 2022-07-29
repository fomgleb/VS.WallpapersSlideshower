using System;
using System.IO;

namespace WallpapersSlideshower.Models
{
    [Serializable]
    public sealed class Wallpaper
    {
        public string FileName { get; }
        public string PathToImage { get; }

        public Wallpaper(string pathToImage)
        {
            PathToImage = pathToImage;
            FileName = Path.GetFileName(pathToImage);
        }
    }
}
