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
            // Create a low-res Sphere (to see the rotations better. It's the vertexbuffer on the videocard)
            var sphere = SpherePrimitives.Sphere(15, 1);


            // # SUN

            // Define the material for the sun (yellow)
            var sunMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Yellow));

            // define an axis angle rotation for the sun (rotate on y-axis)
            var sunRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            // Create a transformation group (to be able to have multiple transformations)
            var sunScaleRotate = new Transform3DGroup();
            // add the scale transform for the sun 
            sunScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(_sunSize, _sunSize, _sunSize)));
            // Add the rotation to the sun.
            sunScaleRotate.Children.Add(new RotateTransform3D(sunRotation));

            // define the sun model (which is a geometry and a material)
            // also assign the sun scale and rotation transform group.
            // This local transform for the sun makes it possible that the sun can rotate and
            // scale independently from the earth/moon,
            var sunModel = new GeometryModel3D
            {
                Material = sunMaterial,
                Geometry = sphere,
                Transform = sunScaleRotate
            };

            // create a sun group, add the earth so it will be relative to the sun.
            //var sunGroup = new Model3DGroup();

            // add the sun itself.
            sunGroup.Children.Add(sunModel);


            // ## Earth

            // create a material for the earth
            var earthMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));

            // define a rotation for earth.
            var earthRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            // we'd like to rotate and scale the earth (without effecting the moon)
            // so we make a separate transform group for this.
            var earthScaleRotate = new Transform3DGroup();
            // add the scale (same geometry only scaled other than the sun)
            earthScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(_earthSize, _earthSize, _earthSize)));
            // Add the rotation to the group.
            earthScaleRotate.Children.Add(new RotateTransform3D(earthRotation));

            // create the earth model (bind geometry + material)
            // use the earth scale/rotate only on the earth model.
            var earthModel = new GeometryModel3D
            {
                Material = earthMaterial,
                Geometry = sphere,
                Transform = earthScaleRotate
            };

            // now we create a new group where the earth and mood are added.
            //var earthGroup = new Model3DGroup();
            // add the earth model
            earthGroup.Children.Add(earthModel);

            // create a rotation for the earth arround the sun.
            var earthArroundSunRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            // the earth is not on the same position as the sun, but translated.
            var earthTransformGroup = new Transform3DGroup();
            // here comes the trick. If you translate first and then rotate, it will be a big
            // elipse. If you rotate first and then translate. the earth is translated and rotate on it self.
            // so first translate, then rotate.
            earthTransformGroup.Children.Add(new TranslateTransform3D(_earthDistance, 0, 0));
            earthTransformGroup.Children.Add(new RotateTransform3D(earthArroundSunRotation));

            // Assign the transform group to the earthgroup.
            earthGroup.Transform = earthTransformGroup;

            // here comes the hiarchy thing! add the earth group to the sungroup.
            sunGroup.Children.Add(earthGroup);


            // ### moon

            // create the moon material.
            var moonMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));

            // define the moon rotation
            var moonRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            // create a moon rotation groep (for the moon itself)
            var moonScaleRotate = new Transform3DGroup();
            // scale it to the size of the moon. (still the same geometry)
            moonScaleRotate.Children.Add(new ScaleTransform3D(new Vector3D(_moonSize, _moonSize, _moonSize)));
            // add the local rotation for the moon.
            moonScaleRotate.Children.Add(new RotateTransform3D(moonRotation));

            // create the moon model.
            var moonModel = new GeometryModel3D
            {
                Material = moonMaterial,
                Geometry = sphere,
                Transform = moonScaleRotate
            };

            // create a group for the rotation of the moon arround earth.
            //var moonGroup = new Model3DGroup();
            // add the model.
            moonGroup.Children.Add(moonModel);

            // define the rotation of the moon arround earth
            var moonArroundEarthRotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            // create transform group for the moon
            var moonTransformGroup = new Transform3DGroup();
            // first translate, then rotate!
            moonTransformGroup.Children.Add(new TranslateTransform3D(_moonDistance, 0, 0));
            moonTransformGroup.Children.Add(new RotateTransform3D(moonArroundEarthRotation));

            // add the transform group to the moonGroup
            moonGroup.Transform = moonTransformGroup;

            // hiarchy thing! add the moon to the earthgroup.
            earthGroup.Children.Add(moonGroup);


            // put the model into the Viewport3D
            Viewport3D.Children.Add(new ModelVisual3D { Content = sunGroup });


            // use a stopwatch (wpf don't render 100% on 60 fps)
            // a stopwatch gives better results.
            var sw = Stopwatch.StartNew();

            // trigger each frame rendering. 
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
