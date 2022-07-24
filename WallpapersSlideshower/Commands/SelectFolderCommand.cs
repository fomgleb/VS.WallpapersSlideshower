using System;
using System.Windows.Forms;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class SelectFolderCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly WallpaperSlideshow _wallpaperSlideshow;

        public SelectFolderCommand(MainWindowViewModel mainWindowViewModel, WallpaperSlideshow wallpaperSlideshow)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _wallpaperSlideshow = wallpaperSlideshow;
        }

        public override bool CanExecute(object parameter) => !_mainWindowViewModel.SlideshowIsEnabled;

        public override void Execute(object parameter)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                SelectedPath = _mainWindowViewModel.PathToFolder,
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;

            _wallpaperSlideshow.GetWallpapersFromFolder(folderBrowserDialog.SelectedPath, System.IO.SearchOption.AllDirectories);
            _mainWindowViewModel.PathToFolder = folderBrowserDialog.SelectedPath;
            _mainWindowViewModel.UpdateWallpapersViewModelsAsync();
        }
    }
}
