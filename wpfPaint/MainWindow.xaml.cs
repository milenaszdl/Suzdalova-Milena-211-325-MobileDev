using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PaintCanvas == null) return;

            var drawingAttributes = PaintCanvas.DefaultDrawingAttributes;
            drawingAttributes.Width = BrushSlider.Value;
            drawingAttributes.Height = BrushSlider.Value;
            PaintCanvas.EraserShape = new EllipseStylusShape(BrushSlider.Value, BrushSlider.Value);
        }

        private void BrushColorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color selectedColor = (Color)(BrushColorCombo.SelectedItem as PropertyInfo).GetValue(null, null);
            PaintCanvas.DefaultDrawingAttributes.Color = selectedColor;
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
    }
}
