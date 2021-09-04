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

namespace PictureDiaShow
{
    public partial class SingleImageView : Window
    {
        public SingleImageView(string imagePath)
        {
            InitializeComponent();

            Image.Source = new BitmapImage(new Uri(imagePath));
            UpdateLayout();
            Focus();
        }
    }
}
