using System;
using System.Windows.Forms;
using WallpapersSlideshower.Models;
using WallpapersSlideshower.ViewModels;

namespace WallpapersSlideshower.Commands
{
    public class SelectFolderCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly WallpapersSlideshow _wallpaperSlideshow;

        public SelectFolderCommand(MainWindowViewModel mainWindowViewModel, WallpapersSlideshow wallpaperSlideshow)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _wallpaperSlideshow = wallpaperSlideshow;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_mainWindowViewModel.SlideshowIsEnabled;
        }


        public override void Execute(object? parameter)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                SelectedPath = _mainWindowViewModel.PathToFolder,
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;
            if (folderBrowserDialog.SelectedPath == null) return;

            _wallpaperSlideshow.GetWallpapersFromFolder(folderBrowserDialog.SelectedPath, System.IO.SearchOption.TopDirectoryOnly);
            _mainWindowViewModel.PathToFolder = folderBrowserDialog.SelectedPath;
            _mainWindowViewModel.UpdateWallpapersViewModels();
        }
    }
}
