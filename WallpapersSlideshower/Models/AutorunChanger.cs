using Microsoft.Win32;
using System.Windows.Forms;

namespace WallpapersSlideshower.Models
{
    public static class AutorunChanger
    {
        public static void Set(bool autorun, string programName)
        {
            string executablePath = Application.ExecutablePath;
            var key = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    key.SetValue(programName, executablePath);
                else
                    key.DeleteValue(programName);

                key.Close();
            }
            catch { }
        }
    }
}