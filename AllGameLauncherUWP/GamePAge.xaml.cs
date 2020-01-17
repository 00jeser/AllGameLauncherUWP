using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Notifications;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.Storage;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AllGameLauncherUWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public Game ThisGame { get; set; }
        public GamePage(Game game)
        {
            ThisGame = game;
            this.InitializeComponent();
            GameNameLable.Text = game.Name;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
            var tileAttributes = tileXml.GetElementsByTagName("text");
            tileAttributes[0].AppendChild(tileXml.CreateTextNode("AllGameLauncher\n"));
            tileAttributes[0].AppendChild(tileXml.CreateTextNode(ThisGame.Name));
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);

            tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare310x310Text01);
            tileAttributes = tileXml.GetElementsByTagName("text");
            tileAttributes[0].AppendChild(tileXml.CreateTextNode("AllGameLauncher\n"));
            tileAttributes[0].AppendChild(tileXml.CreateTextNode(ThisGame.Name));
            tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);


            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\");
            StorageFile sampleFile = await storageFolder.GetFileAsync("start.txt");
            await FileIO.WriteTextAsync(sampleFile, ThisGame.Path);
            await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Launcher");
        }
    }
}
