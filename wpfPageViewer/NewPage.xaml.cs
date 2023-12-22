using Microsoft.Win32;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;

namespace wpfPageViewer
{
    /// <summary>
    /// Логика взаимодействия для NewPage.xaml
    /// </summary>
    public partial class NewPage : Window
    {
        public NewPage()
        {
            InitializeComponent();
        }

        Img[] SelectedPics = new Img[3];

        Img img0 = new Img();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedPics[0] = new Img();
            SelectedPics[1] = new Img();
            SelectedPics[2] = new Img();

            OpenFileDialog ofdPicture = new OpenFileDialog();
            //ofdPicture.Multiselect = true;
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                img0.Source = new Uri(ofdPicture.FileName);
                //int h = 0;
                ////string[] fileNames = new string[3];
                //foreach (string file in ofdPicture.FileNames)
                //{
                //    SelectedPics[h].Source = new Uri(file);
                //    h++;
                //    Console.WriteLine("fhghgh");
                //}


                //int i = 0;
                //foreach (string fileName in fileNames)
                //{
                //    Console.WriteLine(fileName);
                //    SelectedPics[i].Source = new Uri(fileName);
                //    i++;
                //}
            }
        }

        Random rnd = new Random();
        private void CreateCanvas(object sender, RoutedEventArgs e)
        {
            string name = PageName.Text;
            int width = int.Parse(PageWidth.Text);
            int height = int.Parse(PageHeight.Text);
            string color = PageColor.Text;
            Page page = new Page(name, width, height, color);
            Canvas canvas = new Canvas();
            canvas.Width = page.Width;
            canvas.Height = page.Height;
            canvas.Background = Brushes.Aquamarine;

            Image CanvasImage = new Image();
            CanvasImage.Source = new BitmapImage(img0.Source);
            CanvasImage.Width = 100;
            CanvasImage.Height = 100;
            Canvas.SetLeft(CanvasImage, 25);
            Canvas.SetTop(CanvasImage, 25);
            canvas.Children.Add(CanvasImage);


            //Image[] CanvasImages = new Image[3];
            //int j = 0;
            //foreach (Image image in CanvasImages)
            //{
            //    image.Source = new BitmapImage(SelectedPics[j].Source);
            //    image.Width = (canvas.Width / 3);
            //    image.Height = (canvas.Height / 3);
            //    Canvas.SetLeft(image, 25);
            //    Canvas.SetTop(image, 25);
            //    canvas.Children.Add(image);
            //    j++;
            //}


            PlaceForCanvas.Children.Add(canvas);
        }
    }
}
