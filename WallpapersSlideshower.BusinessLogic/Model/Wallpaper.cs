using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpapersSlideshower.BusinessLogic.Model
{
    public class Wallpaper
    {
        private readonly string _path;

        public string Path => _path;

        public Wallpaper(string path)
        {
            _path = path;
        }
    }
}
