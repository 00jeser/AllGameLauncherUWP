using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

namespace AllGameLauncherUWP
{
    class GameButton : Button
    {
        public string GameName { get; set; }
        private Image i = new Image();
        public GameButton(string name)
        {
            GameName = name;


            Grid g = new Grid();
            g.Width = 299;
            g.Margin = new Thickness(-5,0,0,0);
            this.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            g.Children.Add(new TextBlock { Text = name, HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left, Margin = new Windows.UI.Xaml.Thickness(50, 0, 0, 0) });

            var iso = new BitmapImage();
            //iso.UriSource = new Uri("F:/Cora3.png", UriKind.Absolute);

            i.Source = (ImageSource)iso;
            i.Width = 40;
            i.Height = 40;
            i.Margin = new Thickness(0, 0, 0, 0);
            i.HorizontalAlignment = HorizontalAlignment.Left;
            g.Children.Add(i);

            this.Loading += Page_Loading;

            this.Content = g;
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(@"C:\Users\00jes\OneDrive\Изображения\AllGameLauncher\icon\1 — копия (" + GameName + ").png");
            if (file != null)
            {
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    i.Source = bitmapImage;
                }
            }
        }
    }
}
