using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PictureDiaShow
{
    public partial class ImageView : Window
    {
        private List<string> _imagesList;

        private MainWindow _mainWindow;

        Timer _timer;

        public ImageView(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            GetFiles();

            UpdateImage();

            var delay = Convert.ToDouble(_mainWindow.Delay_Select.SelectedItem);
            _timer = new Timer(delay * 1000);
            _timer.Elapsed += TriggerUpdateImage;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void GetFiles()
        {
            _imagesList = new List<string>();

            try
            {
                var allowedEndings = new[] { "png", "jpg", "jpeg", "gif" };
                var imagesPath = _mainWindow.ImageFolder_TextBox.Text;

                foreach (var ending in allowedEndings)
                {
                    _imagesList.AddRange(Directory.GetFiles(imagesPath, $"*.{ending}", SearchOption.AllDirectories));
                }

                _mainWindow.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _mainWindow.History_ListBox.Items.Add($"Found {_imagesList.Count} image files");
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading images with exception:\n\n{ex.Message}");
            }
        }

        private void TriggerUpdateImage(Object source, ElapsedEventArgs e)
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            try
            {
                var randomIndex = new Random().Next(_imagesList.Count);

                Dispatcher.Invoke(new Action(() =>
                {
                    Status_Label.Content = "";
                    Image.Source = new BitmapImage(new Uri(_imagesList[randomIndex]));

                    FileInfo fi = new FileInfo(_imagesList[randomIndex]);
                    ImageName_Label.Content = fi.Name;
                    ImageFolder_Label.Content = fi.Directory;
                    ImageDate_Label.Content = fi.CreationTime.ToString("dd.MM.yyyy HH:mm");

                    UpdateLayout();
                    Focus();
                }));

                Dispatcher.Invoke(new Action(() =>
                {
                    _mainWindow.History_ListBox.Items.Add(_imagesList[randomIndex]);
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not show image because of exception:\n\n{ex}");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Status_Label.Visibility = Visibility.Visible;

            if (e.Key.ToString() == _mainWindow.KeyNext_TextBox.Text)
            {
                _timer.Stop();
                UpdateImage();
                Status_Label.Content = "»";
                _timer.Start();
            }

            else if (e.Key.ToString() == _mainWindow.KeyStop_TextBox.Text)
            {
                if (_timer.Enabled)
                {
                    Status_Label.Content = "■";
                    _timer.Stop();
                }
                else
                {
                    Status_Label.Content = "►";
                    _timer.Start();
                }
            }

            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                Close();
            }
        }
    }
}
