
class Program
{
    static void Main()
    {
        int n = 3; //размер половины ромба

        //первая половина ромба
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= n - i; j++)
            {
                Console.Write(" ");
            }

            Console.Write("/");

            for (int j = 2; j < 2 * i; j++)
            {
                Console.Write(" ");
            }

            Console.Write("\\");

            Console.WriteLine();
        }


        //вторая половина ромба
        for (int i = n ; i >= 1; i--)
        {
            for (int j = 1; j <= n - i; j++)
            {
                Console.Write(" ");
            }

            Console.Write("\\");

            for (int j = 2; j < 2 * i; j++)
            {
                Console.Write(" ");
            }

            Console.Write("/");

            Console.WriteLine();
        }
    }
}