
namespace Program
{
    internal class Class1
    {
        private int length;
        private int width;

        public Class1()
        {
            length = 1;
            width = 2;
        }

        public Class1(int length , int width)
        {
            this.length = length;
            this.width = width;
        }

        internal int GetArea()
        {
            return 123;
        }
    }
}
