using System.Runtime.ExceptionServices;
using System.Text;

namespace cnsGenPassword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count, length;
            string? useDigits, useLowercaseLetters, useUppercaseLetters, useSpecialChars;

            Console.WriteLine("Количество паролей:");
            count = int.Parse(Console.ReadLine());

            Console.WriteLine("Длина каждого пароля:");
            length = int.Parse(Console.ReadLine());

            Console.WriteLine("Использовать цифры в пароле? (y/n)");
            useDigits = Console.ReadLine();

            Console.WriteLine("Использовать маленькие буквы в пароле? (y/n)");
            useLowercaseLetters = Console.ReadLine();

            Console.WriteLine("Использовать большие буквы в пароле? (y/n)");
            useUppercaseLetters = Console.ReadLine();

            Console.WriteLine("Использовать специальные символы? (y/n)");
            useSpecialChars = Console.ReadLine();

            List<string> passwords = GeneratePasswords(count, length, useDigits, useLowercaseLetters, useUppercaseLetters, useSpecialChars);

            foreach (string password in passwords)
            {
                Console.WriteLine(password);
            }
        }

        static List<string> GeneratePasswords(int count, int length, string useDigits, string useLowercaseLetters, string useUppercaseLetters, string useSpecialChars, string customChars = "")
        {
            if (count <= 0 || length <= 0)
            {
                throw new ArgumentException("Count and length should be positive numbers");
            }

            List<string> passwords = new List<string>();
            Random random = new Random();
            string chars="";
            string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            if (useDigits=="y")
            {
                chars += "0123456789";
            }
            if (useLowercaseLetters == "y")
            {
                chars += "abcdefghijklmnopqrstuvwxyz";
            }
            if (useUppercaseLetters == "y")
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            if (useSpecialChars == "y")
            {
                chars += specialChars;
            }

            for (int i = 0; i < count; i++)
            {
                StringBuilder password = new StringBuilder();
                for (int j = 0; j < length; j++)
                {
                    password.Append(chars[random.Next(chars.Length)]);
                }
                passwords.Add(password.ToString());
            }

            return passwords;
        }
    }
}