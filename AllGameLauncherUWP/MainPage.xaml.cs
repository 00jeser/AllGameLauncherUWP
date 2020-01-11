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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AllGameLauncherUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            var r = new Random();
            this.InitializeComponent();
        }

        private void LoadGamePage(object sender, RoutedEventArgs e)
        {
            contentPage.Content = new GamePage((sender as GameButton).ThisGame);
        }

        private void Opener_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            menu.IsPaneOpen = true;
            Opener.Visibility = Visibility.Collapsed;
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            menu.IsPaneOpen = false;
            Opener.Visibility = Visibility.Visible;
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

            IReadOnlyList <StorageFile> fileList = await folder.GetFilesAsync();
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
