using System.Drawing;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Class1 r = new();
            Console.WriteLine(r.GetArea());

            Class1 r2 = new(2, 3);
            Console.WriteLine(r2.GetArea());
        }
    }
}