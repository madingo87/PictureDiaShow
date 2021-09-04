using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace PictureDiaShow
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var delays = new List<string> { "1", "2", "3", "5", "7", "10" };
            delays.ForEach(item => Delay_Select.Items.Add(item));
            Delay_Select.SelectedItem = "3";
        }

        private void Start_Btn_Click(object sender, RoutedEventArgs e)
        {
            ImageView imageView = new ImageView(this);
            imageView.Visibility = Visibility.Visible;
            imageView.WindowState = WindowState.Maximized;
            imageView.Show();
        }

        private void SelectFolder_Btn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImageFolder_TextBox.Text = dialog.SelectedPath;
            }
        }

        private void History_ListBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ShowSingleFile();
            }
        }

        private void History_ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSingleFile();
        }

        private void ShowSingleFile()
        {
            if (History_ListBox.Items.Count > 0 && History_ListBox.SelectedItem != null)
            {
                new SingleImageView(History_ListBox.SelectedItem.ToString()).Show();
            }
        }
    }
}
