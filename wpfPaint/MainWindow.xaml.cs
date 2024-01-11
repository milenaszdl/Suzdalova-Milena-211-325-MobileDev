using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private DrawingAttributes inkDA = new DrawingAttributes { };

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PaintCanvas == null) return;

            var drawingAttributes = PaintCanvas.DefaultDrawingAttributes;
            drawingAttributes.Width = BrushSlider.Value;
            drawingAttributes.Height = BrushSlider.Value;
            currentBrushThickness = BrushSlider.Value;
            PaintCanvas.EraserShape = new EllipseStylusShape(BrushSlider.Value, BrushSlider.Value);
            inkDA.Width = BrushSlider.Value;
            inkDA.Height = BrushSlider.Value;
        }

        private void BrushColorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Color selectedColor = (Color)(BrushColorCombo.SelectedItem as PropertyInfo).GetValue(null, null);
            PaintCanvas.DefaultDrawingAttributes.Color = selectedColor;
            //inkDA.Color = selectedColor;
            //Color selectedColor1 = (Color)ColorConverter.ConvertFromString(BrushColorCombo.SelectedValue.ToString());
            //((SolidColorBrush)currentBrush).Color = selectedColor1;

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

        private bool isDrawing = false;
        private Point startPoint;
        Shape currentShape = null;
        //Color selected = inkDA.Color;
        Brush currentBrush = Brushes.Black;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        double currentBrushThickness = 3;

        enum ToolType { Line, Rect, Ellipse }
        ToolType currentTool;


        private void DrawRectangle_Click(object sender, RoutedEventArgs e)
        {
            currentTool = ToolType.Rect;
            PaintCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void DrawEllipse_Click(object sender, RoutedEventArgs e)
        {
            currentTool = ToolType.Ellipse;
            PaintCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void inkCanvas_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                currentShape = new Line();
            startPoint = e.GetPosition(PaintCanvas);
        }

        //private Rectangle currentRect;
        //private Ellipse currentEllipse;

        //private void inkCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (isDrawing)
        //    {
        //        if (RectButton.IsChecked==true)
        //        {
        //            currentRect = new Rectangle() { Width = Math.Abs(e.GetPosition(PaintCanvas).X - startPoint.X), Height = Math.Abs(e.GetPosition(PaintCanvas).Y - startPoint.Y), Stroke = Brushes.Black, StrokeThickness = 2 };

        //            InkCanvas.SetLeft(currentRect, Math.Min(startPoint.X, e.GetPosition(PaintCanvas).X));
        //            InkCanvas.SetTop(currentRect, Math.Min(startPoint.Y, e.GetPosition(PaintCanvas).Y));
        //            PaintCanvas.Children.Add(currentRect);
        //        }
        //        else if (EllipseButton.IsChecked == true)
        //        {
        //            currentEllipse = new Ellipse() { Width = Math.Abs(e.GetPosition(PaintCanvas).X - startPoint.X), Height = Math.Abs(e.GetPosition(PaintCanvas).Y - startPoint.Y), Stroke = Brushes.Black, StrokeThickness = 2 };
        //            InkCanvas.SetLeft(currentEllipse, Math.Min(startPoint.X, e.GetPosition(PaintCanvas).X));
        //            InkCanvas.SetTop(currentEllipse, Math.Min(startPoint.Y, e.GetPosition(PaintCanvas).Y));
        //            PaintCanvas.Children.Add(currentEllipse);
        //        }
        //        isDrawing = false;
        //    }
        //}

        private void inkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                PaintCanvas.Children.Remove(currentShape);

                switch (currentTool)
                {
                    case ToolType.Line:
                        drawLine(e);
                        break;
                    case ToolType.Rect:
                        drawRect(e);
                        break;
                    case ToolType.Ellipse:
                        drawEllipse(e);
                        break;
                }
            }
            else if (e.LeftButton == MouseButtonState.Released && previousMouseEvent == MouseButtonState.Pressed)
            {
                PaintCanvas.Children.Add(currentShape);
                LineButton.IsChecked = false;
                RectButton.IsChecked = false;
                EllipseButton.IsChecked = false;
            }
            //previousMouseEvent = e.LeftButton;
        }

        private void drawRect(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect.Width = Math.Abs(startPoint.X - e.GetPosition(PaintCanvas).X);
            rect.Height = Math.Abs(startPoint.Y - e.GetPosition(PaintCanvas).Y);
            rect.Stroke = currentBrush;
            rect.StrokeThickness = currentBrushThickness;

            if (startPoint.X - e.GetPosition(PaintCanvas).X > 0)
            {
                InkCanvas.SetLeft(rect, startPoint.X - rect.Width);
            }
            else
            {
                InkCanvas.SetLeft(rect, startPoint.X);
            }

            if (startPoint.Y - e.GetPosition(PaintCanvas).Y > 0)
            {
                InkCanvas.SetTop(rect, startPoint.Y - rect.Height);
            }
            else
            {
                InkCanvas.SetTop(rect, startPoint.Y);
            }

            PaintCanvas.Children.Add(rect);
            currentShape = rect;
        }

        private void drawEllipse(MouseEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Math.Abs(startPoint.X - e.GetPosition(PaintCanvas).X);
            ellipse.Height = Math.Abs(startPoint.Y - e.GetPosition(PaintCanvas).Y);
            ellipse.Stroke = currentBrush;
            ellipse.StrokeThickness = currentBrushThickness;

            if (startPoint.X - e.GetPosition(PaintCanvas).X > 0)
            {
                InkCanvas.SetLeft(ellipse, startPoint.X - ellipse.Width);
            }
            else
            {
                InkCanvas.SetLeft(ellipse, startPoint.X);
            }

            if (startPoint.Y - e.GetPosition(PaintCanvas).Y > 0)
            {
                InkCanvas.SetTop(ellipse, startPoint.Y - ellipse.Height);
            }
            else
            {
                InkCanvas.SetTop(ellipse, startPoint.Y);
            }

            PaintCanvas.Children.Add(ellipse);
            currentShape = ellipse;
        }

        private void drawLine(MouseEventArgs e)
        {
            Line line = new Line();
            line.Stroke = currentBrush;
            line.StrokeThickness = currentBrushThickness;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = e.GetPosition(PaintCanvas).X;
            line.Y2 = e.GetPosition(PaintCanvas).Y;

            PaintCanvas.Children.Add(line);
            currentShape = line;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = ToolType.Line;
            PaintCanvas.EditingMode = InkCanvasEditingMode.None;
        }
    }
}
