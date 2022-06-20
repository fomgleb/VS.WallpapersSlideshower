using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WallpapersSlideshower.BusinessLogic.Controller;

namespace WallpapersSlideshower.WPF
{
    public partial class MainWindow : Window
    {
        private WallpapersController _wallpapersController;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();

            FolderPathTextBox.Text = folderBrowserDialog.SelectedPath;
            _wallpapersController = new WallpapersController(folderBrowserDialog.SelectedPath, SearchOption.AllDirectories);

            foreach (var wallpaper in _wallpapersController.Wallpapers)
            {
                WallpapersListBox.Items.Add(wallpaper.Path);
            }
        }
    }
}
