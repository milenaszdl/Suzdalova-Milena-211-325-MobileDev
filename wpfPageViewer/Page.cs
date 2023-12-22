using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfPageViewer
{
    internal class Page
    {
        public Page(string? name, int width, int height, string? color)
        {
            Name = name;
            Width = width;
            Height = height;
            Color = color;
        }

        public string? Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Color { get; set; }
    }
}
