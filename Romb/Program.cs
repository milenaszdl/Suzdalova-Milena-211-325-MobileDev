
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Закрасить ромб внутри? y/n");
        string? fill = Console.ReadLine();

        int n = 5; //размер половины ромба

        if (fill == "y")
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            //вторая половина ромба
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }


        if (fill == "n") {
            for (int k =0; k < 2 * n - 1; k++)
            {
                Console.Write("*");
            }

            Console.WriteLine();

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write("*");
                }

                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }


            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write("*");
                }

                for (int j = 0; j < 2 * i + 1; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < n - i - 1; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            for (int k = 0; k < 2 * n - 1; k++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Повторить? y/n");
        string? rep = Console.ReadLine();
        if (rep == "y") {
            Main();
        }
    }

}