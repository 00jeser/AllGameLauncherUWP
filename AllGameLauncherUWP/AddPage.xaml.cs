using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AllGameLauncherUWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AddPage : Page
    {
        private Grid Fon;
        private int ocen = -1;
        private string Jenre = "";
        private StorageFile link;
        public AddPage(object fon)
        {
            this.InitializeComponent();
            Fon = fon as Grid;
            Fon.Background = new AcrylicBrush() { BackgroundSource = AcrylicBackgroundSource.HostBackdrop, TintColor = Colors.Black, TintOpacity = 0.2f, TintLuminosityOpacity = 0.5f, AlwaysUseFallback = false, Opacity = 0.7f };

        }

        private void JenreAdd(object sender, RoutedEventArgs e)
        {
            if (JenreSelect.Visibility == Visibility.Collapsed)
            {
                JenreSelect.Visibility = Visibility.Visible;
                JenreInput.Visibility = Visibility.Collapsed;
            }
            else
            {
                JenreSelect.Visibility = Visibility.Collapsed;
                JenreInput.Visibility = Visibility.Visible;
            }
        }

        private void DopPharametr(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = Visibility.Collapsed;
            Dop.Visibility = Visibility.Visible;
        }

        private void SetOcen(object sender, RangeBaseValueChangedEventArgs e)
        {
            ocen = (int)e.NewValue;
        }

        private async void ChoozeFile(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".lnk");
            openPicker.FileTypeFilter.Add(".url");
            openPicker.FileTypeFilter.Add(".exe");
            link = await openPicker.PickSingleFileAsync();
            if (link != null) 
            {
                pathTxt.Text = "[" + link.Name + "]";
                if (Title.Text == "") 
                {
                    Title.Text = link.Name.Split('.')[0];
                }
                if (link.FileType == ".exe")
                    copy.IsChecked = false;
                else
                    copy.IsChecked = true;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\icon\";
            StorageFolder f = await StorageFolder.GetFolderFromPathAsync(path);
            await link.CopyAsync(f);
            }
        }

        private async void Add(object sender, RoutedEventArgs e)
        {

        }
    }
}
