using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AllGameLauncherUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            var r = new Random();
            this.InitializeComponent();
            for (int i = 0; i <= 20; i++)
            {
                var b = new GameButton(i + "")
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

        private void LoadGamePage(object sender, RoutedEventArgs e)
        {
            contentPage.Content = new GamePage((sender as GameButton).GameName);
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
    }
}
