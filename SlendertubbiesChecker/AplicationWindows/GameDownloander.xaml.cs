using SlendertubbiesChecker.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SlendertubbiesChecker.AplicationWindows
{
    /// <summary>
    /// Interaction logic for GameDownloander.xaml
    /// </summary>
    public partial class GameDownloander : Window
    {
        private const string DownloadUrl = "http://Leafq.online/Sebastian/Slendertubbies/Build/Slendertubbies.zip"; // replace with your download URL
        private const string ZipFileName = "Slendertubbies.zip";
        private const string ExtractPath = "Slendertubbies"; // relative path to extract zip file
        private readonly WebClient _webClient = new WebClient();
        private bool _downloadComplete;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private readonly DateTime _startTime = DateTime.Now;

        public GameDownloander()
        {
            InitializeComponent();
            string folderPath = @"Slendertubbies";
            try
            {


                // usuń folder, jeśli już istnieje
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }

                // utwórz nowy folder

            }
            catch (Exception)
            {
                Directory.CreateDirectory(folderPath);
            }
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            DownloadFile();
        }

        private async void DownloadFile()
        {
            try
            {
                _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                _webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                await _webClient.DownloadFileTaskAsync(new Uri(DownloadUrl), ZipFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while downloading the file: {ex.Message}");
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;

            // Calculate download speed and remaining time
            var bytesPerSecond = _webClient.DownloadSpeed();
            var bytesRemaining = totalBytes - bytesIn;
            var secondsRemaining = bytesRemaining / bytesPerSecond;

            {
                //etaLabel.Text = $"{TimeSpan.FromSeconds(secondsRemaining).ToString(@"hh\:mm\:ss")} remaining";
            }

        }

        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            _downloadComplete = true;
            _timer.Stop();
            if (e.Error != null)
            {
                MessageBox.Show($"An error occurred while downloading the file: {e.Error.Message}");
                return;
            }
            ExtractFile();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var timeElapsed = DateTime.Now - _startTime;
            var bytesPerSecond = _webClient.DownloadSpeed();
            var bytesRemaining = _webClient.TotalBytesToReceive() - _webClient.BytesReceived();
            var secondsRemaining = bytesRemaining / bytesPerSecond;
            //etaLabel.Text = $"{timeElapsed.Add(TimeSpan.FromSeconds(secondsRemaining)).ToString(@"hh\:mm\:ss")} remaining";
        }

        private void ExtractFile()
        {
            try
            {
                ZipFile.ExtractToDirectory(ZipFileName, ExtractPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while extracting the file: {ex.Message}");
                return;
            }
            MessageBox.Show("Download complete!");
            Close();
            File.Delete(ZipFileName);
            Thread.Sleep(1000);
            LauncherFilesOperations.PlayGame();
            System.Windows.Application.Current.Shutdown();
        }
    }

    public static class WebClientExtensions
    {
        private static long _lastBytesReceived;
        private static DateTime _lastDownloadTime;

        public static double DownloadSpeed(this WebClient webClient)
        {
            var bytesChange = webClient.BytesReceived() - _lastBytesReceived;
            var timeElapsed = DateTime.Now - _lastDownloadTime;
            var bytesPerSecond = bytesChange / timeElapsed.TotalSeconds;
            _lastBytesReceived = webClient.BytesReceived();
            _lastDownloadTime = DateTime.Now;
            return bytesPerSecond;
        }

        public static long BytesReceived(this WebClient webClient)
        {
            return long.Parse(webClient.ResponseHeaders.Get("Content-Length") ?? "0");
        }
        public static long TotalBytesToReceive(this WebClient webClient)
        {
            return long.Parse(webClient.ResponseHeaders.Get("Content-Length") ?? "0");
        }
    }
}