using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AllGameLauncherUWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        ApplicationDataContainer localSettings;
        public SettingPage()
        {
            this.InitializeComponent();
            localSettings = ApplicationData.Current.LocalSettings;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            localSettings.Values["barStatus"] = (e.AddedItems[0] as ComboBoxItem).Tag;
            Singlton.ChangingSettings();
        }


        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            switch (localSettings.Values["barStatus"])
            {
                case "0":
                    (sender as ComboBox).SelectedIndex = 2;
                    break;
                case "1":
                    (sender as ComboBox).SelectedIndex = 0;
                    break;
                case "2":
                    (sender as ComboBox).SelectedIndex = 1;
                    break;
                default:
                    (sender as ComboBox).Header = localSettings.Values["barStatus"];
                    break;
            }

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListView).SelectedIndex = -1;
        }
    }
}
