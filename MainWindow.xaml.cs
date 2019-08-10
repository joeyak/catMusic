using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace catMusic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _count = 0;

        public MainWindow()
        {
            InitializeComponent();

            Music.MediaEnded += (o, e) =>
            {
                IncrementLoop();
                Music.Position = new TimeSpan();
                Music.Play();
            };

            FilePickerButton.Click += (o, e) =>
            {
                var ofd = new OpenFileDialog();
                if (ofd.ShowDialog() ?? false)
                {
                    Music.Source = new Uri(ofd.FileName);
                    FileName.Content = $"{ofd.SafeFileName}";

                    StartButton.IsEnabled = true;
                }
            };

            StartButton.Click += (o, e) =>
            {
                Music.Play();
                IncrementLoop();
                StartButton.IsEnabled = false;
                StopButton.IsEnabled = true;
            };

            StopButton.Click += (o, e) =>
            {
                Music.Stop();
                StartButton.IsEnabled = true;
                StopButton.IsEnabled = false;
            };

            Prefix.TextChanged += (o, e) => UpdateCounter();
        }

        void IncrementLoop()
        {
            _count++;
            UpdateCounter();
            File.WriteAllText("count.txt", Counter.Content.ToString());
        }

        void UpdateCounter() => Counter.Content = $"{Prefix.Text}: {_count}";
    }
}
