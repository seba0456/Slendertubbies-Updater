using SlendertubbiesChecker.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SlendertubbiesChecker.Functions;
using System.Threading;
using System.IO;
using SlendertubbiesChecker.AplicationWindows;
using System.Windows.Threading;

namespace SlendertubbiesChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            game_files_icon.Source = null;
            game_version_icon.Source = null;
            server_icon.Source = null;
            var networkFunctions = new LauncherNetworkFunctions();
            bool bIsServerAvaliable = networkFunctions.CanConnectToServer();
            //Can acces server?
            if (bIsServerAvaliable)
            {
                server_icon.Source = new BitmapImage(new Uri("Images/check_icon.png", UriKind.Relative));
            }
            else
            {
                server_icon.Source = new BitmapImage(new Uri("Images/x_icon.png", UriKind.Relative));
            }
            //Set icons
            string LocalGameVersion = "Invalid";
            LocalGameVersion = LauncherFilesOperations.CheckLocalVersion("Slendertubbies/TubbyLauncher/LauncherData/gameversion.txt");
            string InternetGameVersion = LauncherNetworkFunctions.ReadTextFromUrl("http://leafq.online/Sebastian/Slendertubbies/Build/gameversion.txt");
            if (LocalGameVersion == "Invalid")
            {
                game_version_icon.Source = new BitmapImage(new Uri("Images/x_icon.png", UriKind.Relative));
            }
            else
            {
                if (LocalGameVersion == InternetGameVersion)
                {
                    game_version_icon.Source = new BitmapImage(new Uri("Images/check_icon.png", UriKind.Relative));
                }
                else
                {
                    game_version_icon.Source = new BitmapImage(new Uri("Images/update_icon.png", UriKind.Relative));
                }
            }
            bool bDoesFileExist = LauncherFilesOperations.DoesFileExists("Slendertubbies/TubbyLauncher/TubbyLauncher.exe");
            if (bDoesFileExist)
            {
                game_files_icon.Source = new BitmapImage(new Uri("Images/check_icon.png", UriKind.Relative));
            }
            else
            {
                game_files_icon.Source = new BitmapImage(new Uri("Images/x_icon.png", UriKind.Relative));
            }

            // Waits 3 seconds
            Dispatcher.InvokeAsync(() =>
            {
                


                Thread.Sleep(3000);
                if (bDoesFileExist)
                {
                    if (LocalGameVersion != InternetGameVersion)
                    {
                        Install window3 = new Install();
                        window3.Show();
                        this.Close();
                    }
                }
                else
                {
                    GameDownloander window4 = new GameDownloander();
                    window4.Show();
                    this.Close();
                }
                if (bDoesFileExist && LocalGameVersion == InternetGameVersion)
                {
                    LauncherFilesOperations.PlayGame();
                    System.Windows.Application.Current.Shutdown();
                }
            }, DispatcherPriority.Background);

        }

    }
}
