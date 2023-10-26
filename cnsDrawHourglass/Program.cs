namespace cnsDrawHourglass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 9;
            DrawSandglass(size);
        }

        static void DrawSandglass(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j < size - i * 2; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            for (int i = size; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                for (int j = 2; j < size - i * 2; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }
    }
}
