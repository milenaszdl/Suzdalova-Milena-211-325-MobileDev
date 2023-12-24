using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            ofdPicture.Multiselect = true;
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                //img0.Source = new Uri(ofdPicture.FileName);

                int h = 0;
                foreach(Img image in SelectedPics)
                {
                    image.Source = new Uri(ofdPicture.FileNames[h]);
                    h++;
                }

            }
        }

        Random rnd = new Random();
        Canvas canvas = new Canvas();
        private void CreateCanvas(object sender, RoutedEventArgs e)
        {
            if (PageName.Text != null && int.TryParse(PageWidth.Text, out int width) && int.TryParse(PageHeight.Text, out int height))
            {
                string name = PageName.Text;
                string back = SelectBack.Text;
                Page page = new Page(name, width, height, back);
                canvas.Name = page.Name;
                canvas.Width = page.Width;
                canvas.Height = page.Height;

                if (back == "С цветами")
                {
                    ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri("C:/Users/milic/source/repos/Suzdalova-Milena-211-325-MobileDev/wpfPageViewer/backgrounds/flowerbackground.jpg"))); // Создаем ImageBrush из выбранного изображения
                    canvas.Background = imageBrush;
                }
                else if (back == "С фоторамками")
                {
                    ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri("C:/Users/milic/source/repos/Suzdalova-Milena-211-325-MobileDev/wpfPageViewer/backgrounds/borderback.jpg"))); // Создаем ImageBrush из выбранного изображения
                    canvas.Background = imageBrush;
                }
                else if (back == "Книга")
                {
                    ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri("C:/Users/milic/source/repos/Suzdalova-Milena-211-325-MobileDev/wpfPageViewer/backgrounds/bookback.jpg"))); // Создаем ImageBrush из выбранного изображения
                    canvas.Background = imageBrush;
                }
                else canvas.Background = Brushes.BurlyWood;

                //canvas.MouseLeftButtonUp += Image_MouseLeftButtonUp;
                //canvas.MouseMove += Image_MouseMove;

                Image[] CanvasImages = new Image[3];
                CanvasImages[0] = new();
                CanvasImages[1] = new();
                CanvasImages[2] = new();
                int j = 0;
                foreach (Image image in CanvasImages)
                {
                    image.Source = new BitmapImage(SelectedPics[j].Source);
                    image.Width = (canvas.Width / 3);
                    image.Height = (canvas.Height / 3);
                    //image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                    Canvas.SetLeft(image, rnd.Next(15, page.Width / 2));
                    Canvas.SetTop(image, rnd.Next(15, page.Width / 2));
                    canvas.Children.Add(image);
                    j++;
                }


                PlaceForCanvas.Children.Add(canvas);
            }
            else MessageBox.Show("Не все параметры заполнены или введены неправильно");
        }

        private Point startPoint;

        //private void Image_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Image image = (Image)sender;
        //    if (image.IsMouseCaptured)
        //    {
        //        Point endPoint = e.GetPosition(canvas);
        //        double left = endPoint.X - startPoint.X;
        //        double top = endPoint.Y - startPoint.Y;
        //        Canvas.SetLeft(image, left);
        //        Canvas.SetTop(image, top);
        //    }
        //}

        //private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    Image image = (Image)sender;
        //    image.ReleaseMouseCapture();
        //}

        //private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    Image image = (Image)sender;
        //    startPoint = e.GetPosition(image);
        //    image.CaptureMouse();
        //}

        private void SaveAsPNG(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in PlaceForCanvas.Children)
            {
                if (element is Canvas)
                {
                    Canvas canvas = (Canvas)element;

                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                        (int)canvas.ActualWidth, (int)canvas.ActualHeight,
                        96d, 96d, PixelFormats.Default);

                    renderBitmap.Render(canvas);

                    string filePath = $"C:/Users/milic/source/repos/Suzdalova-Milena-211-325-MobileDev/wpfPageViewer/savedpages/{canvas.Name}.png"; // Укажите путь к папке, где нужно сохранить изображение
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    using (FileStream file = File.Create(filePath))
                    {
                        encoder.Save(file);
                    }

                    MessageBox.Show("Страница сохранена");

                }
                else MessageBox.Show("Страница пуста, нельзя сохранить");
            }
        }
    }
}
