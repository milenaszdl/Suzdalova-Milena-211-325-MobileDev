using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace wpfPageViewer
{
    internal class Page
    {
        public Page(string? name, int width, int height, string? background)
        {
            Name = name;
            Width = width;
            Height = height;
            Background = background;
        }

        public string? Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Background { get; set; }

        public List<Img> images;

        public void AddImage(Uri imagePath, double x, double y, double imageWidth, double imageHeight)
        {
            images.Add(new Img(imagePath, x, y, imageWidth, imageHeight));
        }
    }
}
