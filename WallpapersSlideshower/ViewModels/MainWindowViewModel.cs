using System.Collections.ObjectModel;
using System.Windows.Input;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.Commands;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace WallpapersSlideshower.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const int WIDTH_OF_ICON_RESOLUTION = 150;

        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public ObservableCollection<WallpaperViewModel> WallpapersViewModels { get; set; }

        public string PathToFolder { get => _pathToFolder; set => Set(ref _pathToFolder, value); }
        private string _pathToFolder;

        public WallpaperViewModel SelectedWallpaperViewModel { get => _selectedWallpaperViewModel; set => Set(ref _selectedWallpaperViewModel, value); }
        private WallpaperViewModel _selectedWallpaperViewModel;

        public bool SlideshowIsntEnabled => !_slideshowIsEnabled;
        public bool SlideshowIsEnabled { get => _slideshowIsEnabled; set => Set(ref _slideshowIsEnabled, value); }
        private bool _slideshowIsEnabled;

        public ICommand SelectFolderCommand { get; }
        public ICommand SetSlideshowEnabledCommand { get; }

        public MainWindowViewModel(WallpaperSlideshow wallpaperSlideshow)
        {
            _wallpaperSlideshow = wallpaperSlideshow;
            WallpapersViewModels = new ObservableCollection<WallpaperViewModel>();
            WallpapersViewModels.CollectionChanged += OnWallpapersViewModelsChanged;

            _pathToFolder = wallpaperSlideshow.PathToWallpapersFolder;

            SelectFolderCommand = new SelectFolderCommand(this, _wallpaperSlideshow);
            SetSlideshowEnabledCommand = new SetSlideshowEnabledCommand(this, _wallpaperSlideshow);

            UpdateWallpapersViewModels();
        }

        public void UpdateWallpapersViewModels()
        {
            WallpapersViewModels.CollectionChanged -= OnWallpapersViewModelsChanged;
            WallpapersViewModels.Clear();
            foreach (var wallpaper in _wallpaperSlideshow.Wallpapers)
            {
                var wallpaperViewModel = new WallpaperViewModel(wallpaper.PathToImage, WIDTH_OF_ICON_RESOLUTION);
                WallpapersViewModels.Add(wallpaperViewModel);
            }

            WallpapersViewModels.CollectionChanged -= OnWallpapersViewModelsChanged;
            WallpapersViewModels.CollectionChanged += OnWallpapersViewModelsChanged;

            _currentLoadAllIconImagesCancellationTockenSource?.Cancel();
            _currentLoadAllIconImagesCancellationTockenSource = new CancellationTokenSource();
            LoadAllIconImagesAsync(_currentLoadAllIconImagesCancellationTockenSource);
        }

        private CancellationTokenSource _currentLoadAllIconImagesCancellationTockenSource;
        private async void LoadAllIconImagesAsync(CancellationTokenSource cancellationTokenSource)
        {
            var wallpapersViewModelsCopy = new WallpaperViewModel[WallpapersViewModels.Count];
            WallpapersViewModels.CopyTo(wallpapersViewModelsCopy, 0);
            foreach (var wallpaperViewModel in wallpapersViewModelsCopy)
            {
                await wallpaperViewModel.LoadIconImageAsync();
                if (cancellationTokenSource.IsCancellationRequested)
                    return;
            }
        }

        private void OnWallpapersViewModelsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _wallpaperSlideshow.Wallpapers.Clear();
            foreach (var wallpaperViewModel in WallpapersViewModels)
                _wallpaperSlideshow.Wallpapers.Add(new Wallpaper(wallpaperViewModel.PathToImage));
        }
    }
}
