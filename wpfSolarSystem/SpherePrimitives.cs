using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace wpfSolarSystem
{
    internal class SpherePrimitives
    {
        public static MeshGeometry3D Sphere(int slices, double radius)
        {
            var mesh = new MeshGeometry3D();
            var n = slices / 2;
            var r = radius;
            int e;
            var segmentRad = Math.PI / 2 / (n + 1);
            var numberOfSeparators = 4 * n + 4;

            for (e = -n; e <= n; e++)
            {
                var r_e = -r * Math.Cos(segmentRad * e);
                var y_e = -r * Math.Sin(segmentRad * e);

                for (var s = 0; s <= numberOfSeparators - 1; s++)
                {
                    var z_s = -r_e * Math.Sin(segmentRad * s) * -1;
                    var x_s = -r_e * Math.Cos(segmentRad * s);
                    mesh.Positions.Add(new Point3D(x_s, y_e, z_s));
                }
            }

            mesh.Positions.Add(new Point3D(0, -1 * r, 0));
            mesh.Positions.Add(new Point3D(0, r, 0));

            for (e = 0; e < 2 * n; e++)
                for (var i = 0; i < numberOfSeparators; i++)
                {
                    mesh.TriangleIndices.Add(e * numberOfSeparators + i);
                    mesh.TriangleIndices.Add(e * numberOfSeparators + i +
                                             numberOfSeparators);
                    mesh.TriangleIndices.Add(e * numberOfSeparators + (i + 1) %
                                             numberOfSeparators + numberOfSeparators);

                    mesh.TriangleIndices.Add(e * numberOfSeparators + (i + 1) %
                                             numberOfSeparators + numberOfSeparators);
                    mesh.TriangleIndices.Add(e * numberOfSeparators +
                                             (i + 1) % numberOfSeparators);
                    mesh.TriangleIndices.Add(e * numberOfSeparators + i);
                }

            for (var i = 0; i < numberOfSeparators; i++)
            {
                mesh.TriangleIndices.Add(e * numberOfSeparators + i);
                mesh.TriangleIndices.Add(numberOfSeparators * (2 * n + 1));
                mesh.TriangleIndices.Add(e * numberOfSeparators + (i + 1) %
                                         numberOfSeparators);
            }

            for (var i = 0; i < numberOfSeparators; i++)
            {
                mesh.TriangleIndices.Add(i);
                mesh.TriangleIndices.Add((i + 1) % numberOfSeparators);
                mesh.TriangleIndices.Add(numberOfSeparators * (2 * n + 1) + 1);
            }

            return mesh;
        }
    }
}
