using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AllGameLauncherUWP
{
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public MainPage()
        {
            var r = new Random();
            this.InitializeComponent();
            Singlton.SettingChange += SettingsChanged;
            SettingsChanged();
        }

        private void SettingsChanged()
        {
            Opener.Visibility = Visibility.Visible;
            switch ((localSettings.Values["barStatus"] as string))
            {
                case "2":
                    Opener.Visibility = Visibility.Collapsed;
                    menu.IsPaneOpen = false;
                    break;
                case "1":
                    Opener.Visibility = Visibility.Collapsed;
                    menu.IsPaneOpen = true;
                    break;
                case "0":
                    menu.IsPaneOpen = false;
                    break;
            }
        }

        private async void LoadGamePage(object sender, RoutedEventArgs e)
        {
            contentPage.Content = new GamePage((sender as GameButton).ThisGame);
            fon.Background = null;
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\background\" + (sender as GameButton).ThisGame.Name + ".img";
                StorageFile file = await StorageFile.GetFileFromPathAsync(path);
                if (file != null)
                {
                    var stream = await file.OpenAsync(FileAccessMode.Read);

                    var bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);

                    fon.Background = new ImageBrush
                    {
                        ImageSource = bitmapImage,
                        Stretch = Stretch.UniformToFill
                    };
                    bitmapImage.AutoPlay = true;
                }
            }
            catch { }
        }

        private void Opener_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var a = localSettings.Values["barStatus"];
            if ((a as string) != "2")
            {
                menu.IsPaneOpen = true;
                Opener.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var a = localSettings.Values["barStatus"];
            if ((a as string) == "0")
            {
                menu.IsPaneOpen = false;
                Opener.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            contentPage.Content = new SettingPage();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\games\");

            //Windows.Storage.StorageFile sampleFile = await folder.CreateFileAsync("sample.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //await Windows.Storage.FileIO.WriteTextAsync(sampleFile, JsonConvert.SerializeObject(new Game { Name = "123", Path= @"G:\Games\Dead Space\Dead Space.exe", Genre="123", Add=DateTime.Now, Ocen="100" }));

            IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();
            List<Game> games = new List<Game>();

            foreach (StorageFile file in fileList)
            {
                string text = await FileIO.ReadTextAsync(file);
                games.Add(JsonConvert.DeserializeObject<Game>(text));
            }

            foreach (Game g in games)
            {
                var b = new GameButton(g)
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(-12, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = 301,
                    Height = 50
                };
                b.Click += LoadGamePage;
                Games.Items.Add(b);
            }
        }

    }
}
