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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlatBedModeling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        AxisAngleRotation3D axx3d;
        AxisAngleRotation3D axy3d;
        AxisAngleRotation3D axz3d;

        public MainWindow()
        {
            InitializeComponent();

            var surface = new MeshGeometry3D();
            surface.Positions.Add(new Point3D(0, 0, 0));
            surface.Positions.Add(new Point3D(1, 21, 3));
            surface.Positions.Add(new Point3D(0, 40, 6));
            surface.Positions.Add(new Point3D(50, -1, -1));
            surface.Positions.Add(new Point3D(50, 20, -4));
            surface.Positions.Add(new Point3D(49, 41, 0));
            surface.Positions.Add(new Point3D(100, -4, 0));
            surface.Positions.Add(new Point3D(101, 18, 2));
            surface.Positions.Add(new Point3D(104, 35, 3));
            surface.TriangleIndices.Add(0);
            surface.TriangleIndices.Add(1);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(0);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(3);
            surface.TriangleIndices.Add(1);
            surface.TriangleIndices.Add(2);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(2);
            surface.TriangleIndices.Add(5);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(8);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(5);
            surface.TriangleIndices.Add(8);
            surface.TriangleIndices.Add(7);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(7);
            surface.TriangleIndices.Add(6);
            surface.TriangleIndices.Add(4);
            surface.TriangleIndices.Add(6);
            surface.TriangleIndices.Add(3);
            surface.TriangleIndices.Add(4);
            //FlatBed1.MeshGeometry = surface;
            //FlatBed1.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
            //FlatBed1.Transform = new TranslateTransform3D(-50,-20,0);


            FlatBed2.MeshGeometry = surface;
            FlatBed2.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
            axx3d = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            axy3d = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            axz3d = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);
            RotateTransform3D myxRotateTransform = new RotateTransform3D(axx3d, surface.Positions[surface.Positions.Count / 2]);

            RotateTransform3D myyRotateTransform = new RotateTransform3D(axy3d, surface.Positions[surface.Positions.Count / 2]);

            RotateTransform3D myzRotateTransform = new RotateTransform3D(axz3d, surface.Positions[surface.Positions.Count / 2]);
            FlatBed2.Transform = new Transform3DGroup
            {
                Children =
                    new Transform3DCollection(new List<Transform3D>
                    {
                        myxRotateTransform,
                        myyRotateTransform,
                        myzRotateTransform
                    })
            };
            XSlider.ValueChanged += SliderOnValueChanged;
            YSlider.ValueChanged += SliderOnValueChanged;
            ZSlider.ValueChanged += SliderOnValueChanged;
            Camera.Position = new Point3D(50, 20, -100);
            Camera.LookDirection = new Vector3D(0, 0, 1);
            Camera.UpDirection = new Vector3D(1, 0, 0);
            Camera.FieldOfView = 90;

        }

        private void SliderOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> routedPropertyChangedEventArgs)
        {
            var name = (sender as Slider).Name;
            if (name == "XSlider")
                axx3d.Angle = routedPropertyChangedEventArgs.NewValue;

            if (name == "YSlider")
                axy3d.Angle = routedPropertyChangedEventArgs.NewValue;

            if (name == "ZSlider")
                axz3d.Angle = routedPropertyChangedEventArgs.NewValue;
            FlatBed2.UpdateModel();
        }
      
    }
}
