using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPageViewer
{
    internal class Img
    {
        public Img()
        {
        }

        public Uri? Source { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Img(Uri path, double x, double y, double width, double height)
        {
            Source = path;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
