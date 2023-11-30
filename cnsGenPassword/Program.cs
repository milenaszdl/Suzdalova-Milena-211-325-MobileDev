using System.Runtime.ExceptionServices;

namespace cnsGenPassword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Количество паролей?");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Количество символов в пароле?"); 
            int k = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Использовать цифры? y/n");
            string? num = Console.ReadLine();

            Console.WriteLine("Использовать маленькие буквы? y/n");
            string? mal = Console.ReadLine();

            Console.WriteLine("Использовать большие буквы? y/n");
            string? bol = Console.ReadLine();

            Console.WriteLine("Использовать спец символы? y/n");
            string? spec = Console.ReadLine();

            string usersymbol;

            if (spec=="y")
            {
                Console.WriteLine("Какой спец символ использовать?");
                usersymbol = Console.ReadLine();
            } 
            else
            {
                usersymbol = String.Empty;
            }

            GenPassword(n, k, num, mal);
        }

        //string num, string mal, string bol, string spec, string usersymbol
        public static void GenPassword(int n, int k, string num, string mal)
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (num =="y")
                    {
                        int newnum = rnd.Next(0, 9);
                        Console.Write(newnum);
                    } else if (mal == "y")
                    {
                        int newletter = rnd.Next(0, 26);
                        char rndletter = (char)('a' + newletter);
                        Console.Write(rndletter);
                    } else 
                }
                Console.WriteLine();
            }
        }
    }
}