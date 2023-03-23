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
using System.Windows.Shapes;

namespace SlendertubbiesChecker.AplicationWindows
{
    /// <summary>
    /// Interaction logic for Install.xaml
    /// </summary>
    public partial class Install : Window
    {
        public Install()
        {
            InitializeComponent();
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            LauncherFilesOperations.PlayGame();
            System.Windows.Application.Current.Shutdown();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            GameDownloander window2 = new GameDownloander();
            window2.Show();
            this.Close();
        }
    }
}
