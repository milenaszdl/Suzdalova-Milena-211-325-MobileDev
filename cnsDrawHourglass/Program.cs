namespace cnsDrawHourglass
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Нарисовать песочные часы символом * (введите 1) или цифрами (введите 2) : ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if (choice == 1)
            {
                PrintStarHourglass();
            }
            if (choice == 2)
            {
                PrintNumHourglass();
            }

            Console.WriteLine();
            Console.WriteLine("Повторить? y/n");
            string? repeat = Console.ReadLine();
            if (repeat=="y")
            {
                Main();
            }
        }

        static void PrintStarHourglass()
        {
            int n = 9; // количество цифр в первой строке

            // первую половина фигуры
            for (int i = n; i >= 1; i -= 2)
            {
                PrintRowStar(i, n);
            }

            // вторая половина фигуры
            for (int i = 3; i <= n; i += 2)
            {
                PrintRowStar(i, n);
            }
        }

        static void PrintRowStar(int numCount, int totalNumCount)
        {
            int spaceCount = (totalNumCount - numCount) / 2; //для пробелов

            for (int i = 0; i < spaceCount; i++)
            {
                Console.Write(" ");
            }

            for (int i = 0; i < numCount; i++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        static void PrintNumHourglass()
        {
            int n = 9; // количество цифр в первой строке
            int k = 5; //выводимая цифра 

            // первую половина фигуры
            for (int i = n; i >= 1; i -= 2)
            {
                PrintRow(k, i, n);
                k--;
            }

            k = k + 2;

            // вторая половина фигуры
            for (int i = 3; i <= n; i += 2)
            {
                PrintRow(k, i, n);
                k++;
            }
        }

        static void PrintRow(int cifra, int numCount, int totalNumCount)
        {
            int spaceCount = (totalNumCount - numCount) / 2; //для пробелов

            for (int i = 0; i < spaceCount; i++)
            {
                Console.Write(" ");
            }

            for (int i = 0; i < numCount; i++)
            {
                Console.Write(cifra);
            }

            Console.WriteLine();
        }
    }
}
