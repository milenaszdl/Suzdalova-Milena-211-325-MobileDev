namespace cnsEnum
{
    internal class Program
    {

        public enum SingleColor {  Red, Green, Blue = 100500, White };

        public enum MultiColor : byte { 
            Red = 1,
            Green = 2, 
            Blue = 4
        };

        static void Main(string[] args)
        {
            SingleColor singleColor = SingleColor.Red;
            Console.WriteLine(singleColor);
            Console.WriteLine(singleColor.ToString("D"));
            singleColor = SingleColor.Blue;
            Console.WriteLine(singleColor.ToString("D"));

            MultiColor multiColor = MultiColor.Red;
            multiColor |= MultiColor.Blue; //добавляю второй признак в маркер multiColor. он и красный и синий
            Console.WriteLine(multiColor);
            //multiColor ^= MultiColor.Blue; //удаляем
            Console.WriteLine(multiColor);

            Console.WriteLine(multiColor == MultiColor.Red);
            Console.WriteLine(multiColor.HasFlag(MultiColor.Red));
        }
    }
}