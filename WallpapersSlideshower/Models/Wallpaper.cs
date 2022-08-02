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

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            if (obj is not Wallpaper other) return false;
            return other.FileName == FileName && other.PathToImage == PathToImage;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FileName, PathToImage);
        }
    }
}
