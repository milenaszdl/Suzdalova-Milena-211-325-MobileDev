using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace wpfPageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = System.IO.Path.Combine("C:/Users/milic/source/repos/Suzdalova-Milena-211-325-MobileDev/wpfPageViewer/savedpages/");

            if (Directory.Exists(imagePath))
            {
                var imageFiles = Directory.GetFiles(imagePath, "*.png") ;
                List<string> imageList = new List<string>();
                foreach (var imageFile in imageFiles)
                {
                    imageList.Add(System.IO.Path.GetFileName(imageFile));
                }
                pagesListBox.ItemsSource = imageList;
            }
        }

        private void NewPage_Click(object sender, RoutedEventArgs e)
        {
            NewPage newPageWindow = new NewPage();
            newPageWindow.Show();
        }

        private void pagesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (pagesListBox.SelectedItem != null)
            {
                string? fileName = pagesListBox.SelectedItem.ToString();
                string imagePath = System.IO.Path.Combine($"C:/Users/milic/source/repos/Suzdalova-Milena-211-325-MobileDev/wpfPageViewer/savedpages/{fileName}");
                if (System.IO.File.Exists(imagePath))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                    SelectedPage.Source = bitmap;
                }
            }
        }
    }
}
