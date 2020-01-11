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
        public Game ThisGame { get; set; }
        private Image i = new Image();
        public GameButton(Game game)
        {
            ThisGame = game;


            Grid g = new Grid();
            g.Width = 299;
            g.Margin = new Thickness(-5,0,0,0);
            this.HorizontalContentAlignment = HorizontalAlignment.Left;
            g.Children.Add(new TextBlock { Text = ThisGame.Name, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(50, 0, 0, 0) });

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
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\icon\" + ThisGame.Name + ".png";
                StorageFile file = await StorageFile.GetFileFromPathAsync(path);
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
            catch
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\icon\logo.png");
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
}
