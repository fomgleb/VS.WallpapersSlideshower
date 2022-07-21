using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using WallpapersSlideshower.Models;
using WPFTesting.Infrastructure.Commands;

namespace WallpapersSlideshower.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public ObservableCollection<Wallpaper> Wallpapers { get; set; }

        private string _pathToFolder;
        public string PathToFolder
        {
            get => _pathToFolder;
            set => Set(ref _pathToFolder, value);
        }

        private Wallpaper _selectedWallpaper;
        public Wallpaper SelectedWallpaper
        {
            get => _selectedWallpaper;
            set => Set(ref _selectedWallpaper, value);
        }

        public ICommand SelectFolderCommand { get; }
        private bool CanSelectFolderCommandExecute(object p) => true;
        private void OnSelectFolderCommandExecuted(object p)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            _wallpaperSlideshow.SelectWallpapersFromFolder(folderBrowserDialog.SelectedPath, System.IO.SearchOption.AllDirectories);
            PathToFolder = folderBrowserDialog.SelectedPath;

            Microsoft.Win32.SystemEvents.PowerModeChanged += (s, e) =>
            {
                if (e.Mode != Microsoft.Win32.PowerModes.Resume) return;
                _wallpaperSlideshow.SwitchToNextWalppaper();
                _wallpaperSlideshow.ShowCurrentWallpaper();
            };
        }

        public MainWindowViewModel(WallpaperSlideshow wallpaperSlideshow)
        {
            _wallpaperSlideshow = wallpaperSlideshow;

            SelectFolderCommand = new ActionCommand(OnSelectFolderCommandExecuted, CanSelectFolderCommandExecute);

            Wallpapers = wallpaperSlideshow.Wallpapers;
        }
    }
}
