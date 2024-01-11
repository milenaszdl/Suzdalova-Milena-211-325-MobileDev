using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BrushColorCombo.ItemsSource = typeof(Colors).GetProperties();
            PropertyInfo[] colors = BrushColorCombo.ItemsSource.Cast<PropertyInfo>().ToArray();
        }

        //private var drawingAttributes = PaintCanvas.DefaultDrawingAttributes;
        private DrawingAttributes inkDA = new DrawingAttributes {};

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PaintCanvas == null) return;

            var drawingAttributes = PaintCanvas.DefaultDrawingAttributes;
            drawingAttributes.Width = BrushSlider.Value;
            drawingAttributes.Height = BrushSlider.Value;
            PaintCanvas.EraserShape = new EllipseStylusShape(BrushSlider.Value, BrushSlider.Value);
            inkDA.Width = BrushSlider.Value;
            inkDA.Height = BrushSlider.Value;
        }

        private void BrushColorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color selectedColor = (Color)(BrushColorCombo.SelectedItem as PropertyInfo).GetValue(null, null);
            PaintCanvas.DefaultDrawingAttributes.Color = selectedColor;
            inkDA.Color = selectedColor;
        }

        private void BrushStateCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaintCanvas == null) return;

            switch (BrushStateCombo.SelectedIndex)
            {
                case 0:
                    PaintCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case 1:
                    PaintCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
            }
        }

        private void SaveAsPNG(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)PaintCanvas.ActualWidth, (int)PaintCanvas.ActualHeight, 96, 96, System.Windows.Media.PixelFormats.Default);
                    renderTargetBitmap.Render(PaintCanvas);

                    PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                    pngEncoder.Save(fileStream);
                }
            }
        }

        private Point currentPoint;
        private Point startPoint;
        private void inkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(PaintCanvas);
        }

        private void inkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point endPoint = e.GetPosition(PaintCanvas);
            if (e.LeftButton == MouseButtonState.Released)
            {
                if (endPoint != startPoint)
                {
                    currentPoint = endPoint;
                }
            }
        }

        private void DrawRectangle_Click(object sender, RoutedEventArgs e)
        {
            PaintCanvas.DefaultDrawingAttributes = inkDA;
            var rectAttributes = PaintCanvas.DefaultDrawingAttributes;
            rectAttributes.Width = BrushSlider.Value;
            rectAttributes.Height = BrushSlider.Value;
            Color selectedColor = (Color)(BrushColorCombo.SelectedItem as PropertyInfo).GetValue(null, null);
            PaintCanvas.DefaultDrawingAttributes.Color = selectedColor;
            rectAttributes.Color = PaintCanvas.DefaultDrawingAttributes.Color;
            StylusPointCollection points = new StylusPointCollection();
            points.Add(new StylusPoint(currentPoint.X, currentPoint.Y));
            points.Add(new StylusPoint(currentPoint.X + 200, currentPoint.Y));
            points.Add(new StylusPoint(currentPoint.X + 200, currentPoint.Y + 100));
            points.Add(new StylusPoint(currentPoint.X, currentPoint.Y + 100));
            points.Add(new StylusPoint(currentPoint.X, currentPoint.Y));
            Stroke s = new Stroke(points);
            PaintCanvas.Strokes.Add(s);
        }

        private void DrawEllipse_Click(object sender, RoutedEventArgs e)
        {
            PaintCanvas.DefaultDrawingAttributes = inkDA;
            StylusPointCollection points = new StylusPointCollection();
            for (double theta = 0; theta < 2 * Math.PI; theta += 0.01)
            {
                double x = currentPoint.X + 100 * Math.Cos(theta);
                double y = currentPoint.Y + 50 * Math.Sin(theta);
                points.Add(new StylusPoint(x, y));
            }
            Stroke s = new Stroke(points);
            PaintCanvas.Strokes.Add(s);
        }
    }
}
