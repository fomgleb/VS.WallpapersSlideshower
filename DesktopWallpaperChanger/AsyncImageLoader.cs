using System.Windows;
using System.Windows.Controls;

namespace DesktopWallpaper
{
    static class AsyncImageLoader
    {
        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static string GetSource(Image img)
        {
            return (string)img.GetValue(SourceProperty);
        }
        public static void SetSource(Image img, object value)
        {
            img.SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached("Source", typeof(string), typeof(AsyncImageLoader), new UIPropertyMetadata(string.Empty, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // Здесь запускается отдельный поток для загрузки изображения любым удобным для вас способом. Путь к файлу = e.NewValue, ссылка на Image = obj
        }
    }
}
