using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WallpapersSlideshower.ViewModels
{
    public class WallpaperViewModel : ViewModelBase
    {
        public ImageSource? IconImageSource{ get => _iconImageSource; set => Set(ref _iconImageSource, value); }
        private ImageSource? _iconImageSource;

        public string FileName { get; }
        public string PathToImage { get; }

        private readonly int _widthOfIconResolution;

        public WallpaperViewModel(string pathToImage, int widthOfIconResolution)
        {
            PathToImage = pathToImage;
            FileName = Path.GetFileName(pathToImage);
            _widthOfIconResolution = widthOfIconResolution;
        }

        public async Task LoadIconImageAsync()
        {
            IconImageSource = await Task.Run(() =>
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(PathToImage);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.DecodePixelWidth = _widthOfIconResolution;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            });
        }
    }
}
