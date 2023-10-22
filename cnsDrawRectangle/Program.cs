namespace cnsDrawRectangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ширина фигуры?");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Высота фигуры?");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Символ рисования?");
            string? symbol = Console.ReadLine();
            Console.WriteLine("Заполнить фигуру? y/n");
            string? yn = Console.ReadLine();

            if (yn == "y")
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write(symbol);
                    }
                    Console.WriteLine();
                }
            }
            else if (yn == "n")
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(symbol);
                }
                Console.WriteLine();

                for (int k = 0; k < (height-2); k++)
                {
                    Console.Write(symbol);
                    for (int j = 0; j < (width - 2); j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(symbol);
                    Console.WriteLine();
                }

                for (int j = 0; j < width; j++)
                {
                    Console.Write(symbol);
                }
            }
        }
    }
}