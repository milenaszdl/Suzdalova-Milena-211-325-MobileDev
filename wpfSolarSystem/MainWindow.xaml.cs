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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace wpfSolarSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private const double _sunSize = 70.0;
        private const double _earthSize = 20.0;
        private const double _moonSize = 10.0;

        private const double _earthDistance = 170.0;
        private const double _moonDistance = 40.0;

        //var sunGroup = new Model3DGroup();
        static Model3DGroup sunGroup = new Model3DGroup();
        static Model3DGroup earthGroup = new Model3DGroup();
        static Model3DGroup moonGroup = new Model3DGroup();

        public MainWindow()
        {
            InitializeComponent();
            SetupScene();
        }

        private void SetupScene()
        {
            //создаем сферу для геометрии моделей
            var sphere = SpherePrimitives.Sphere(15, 1);

            // СОЛНЦЕ

            //материал
            var sunMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Yellow));

            // вращение вокруг своей оси Y
            var sunRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            //задаем размеры
            var sunScaleRotate = new Transform3DGroup();
            sunScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(_sunSize, _sunSize, _sunSize)));
            sunScaleRotate.Children.Add(new RotateTransform3D(sunRotation));

            //создаем модель
            var sunModel = new GeometryModel3D
            {
                Material = sunMaterial,
                Geometry = sphere,
                Transform = sunScaleRotate
            };

            // добавляем в группу
            sunGroup.Children.Add(sunModel);


            // ЗЕМЛЯ

            var earthMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));

            var earthRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            var earthScaleRotate = new Transform3DGroup();
            earthScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(_earthSize, _earthSize, _earthSize)));
            earthScaleRotate.Children.Add(new RotateTransform3D(earthRotation));

            var earthModel = new GeometryModel3D
            {
                Material = earthMaterial,
                Geometry = sphere,
                Transform = earthScaleRotate
            };

            earthGroup.Children.Add(earthModel);

            // вращение вокруг солнца
            var earthArroundSunRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            //задаем расстояние до солнца (центра координат) и добавляем ранее созданное вращение
            var earthTransformGroup = new Transform3DGroup();
            earthTransformGroup.Children.Add(new TranslateTransform3D(_earthDistance, 0, 0));
            earthTransformGroup.Children.Add(new RotateTransform3D(earthArroundSunRotation));

            earthGroup.Transform = earthTransformGroup;

            sunGroup.Children.Add(earthGroup);


            // ЛУНА

            var moonMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));

            var moonRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            var moonScaleRotate = new Transform3DGroup();
            moonScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(_moonSize, _moonSize, _moonSize)));
            moonScaleRotate.Children.Add(new RotateTransform3D(moonRotation));


            var moonModel = new GeometryModel3D
            {
                Material = moonMaterial,
                Geometry = sphere,
                Transform = moonScaleRotate
            };

            moonGroup.Children.Add(moonModel);

            var moonArroundEarthRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            var moonTransformGroup = new Transform3DGroup();
            moonTransformGroup.Children.Add(new TranslateTransform3D(_moonDistance, 0, 0));
            moonTransformGroup.Children.Add(new RotateTransform3D(moonArroundEarthRotation));

            moonGroup.Transform = moonTransformGroup;

            earthGroup.Children.Add(moonGroup);


            // добавляем группу с солнцем землей и луной на вьюпорт
            Viewport3D.Children.Add(new ModelVisual3D { Content = sunGroup });


            // для рендера
            var sw = Stopwatch.StartNew();

            CompositionTarget.Rendering += (s, ee) =>
            {
                // get a snapshot of the current time.
                var time = sw.ElapsedMilliseconds;

                earthRotation.Angle = time / 6.0;
                earthArroundSunRotation.Angle = time / 24.0;

                moonRotation.Angle = time / 15.0;
                moonArroundEarthRotation.Angle = time / 4.0;

                sunRotation.Angle = -time / 7.0;
            };
        }

        private void CreatePlanet(object sender, RoutedEventArgs e)
        {
            if (PlanetRad.Text != null && double.TryParse(PlanetRad.Text, out double newrad) && PlanetDistance.Text != null && double.TryParse(PlanetDistance.Text, out double newdist))
            {

                var newsphere = SpherePrimitives.Sphere(15, 1);

                SolidColorBrush myBrush = new SolidColorBrush();
                string newcolor = SelectColor.Text;
                if (newcolor == "Зеленый")
                {
                    myBrush.Color = Colors.Green;
                } else if (newcolor == "Красный")
                {
                    myBrush.Color = Colors.Red;
                } else if (newcolor == "Оранжевый")
                {
                    myBrush.Color = Colors.Orange;
                } else if (newcolor == "Голубой")
                {
                    myBrush.Color = Colors.LightSkyBlue;
                }
                var newMaterial = new DiffuseMaterial(myBrush);

                var newRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

                var newScaleRotate = new Transform3DGroup();
                newScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(newrad, newrad, newrad)));
                newScaleRotate.Children.Add(new RotateTransform3D(newRotation));

                
                var newModel = new GeometryModel3D
                {
                    Material = newMaterial,
                    Geometry = newsphere,
                    Transform = newScaleRotate
                };

                
                var newGroup = new Model3DGroup();
                newGroup.Children.Add(newModel);

                var newArroundSunRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

                var newTransformGroup = new Transform3DGroup();
                newTransformGroup.Children.Add(new TranslateTransform3D(newdist, 0, 0)); //distance
                newTransformGroup.Children.Add(new RotateTransform3D(newArroundSunRotation));

                newGroup.Transform = newTransformGroup;

                sunGroup.Children.Add(newGroup);

                Viewport3D.Children.Add(new ModelVisual3D { Content = sunGroup });

                var sw = Stopwatch.StartNew();

                CompositionTarget.Rendering += (s, ee) =>
                {
                    var time = sw.ElapsedMilliseconds;

                    newRotation.Angle = time / 6.0;
                    newArroundSunRotation.Angle = time / 24.0;
                };

            } else MessageBox.Show("Не все параметры заполнены или введены неправильно");
        }
    }
}
