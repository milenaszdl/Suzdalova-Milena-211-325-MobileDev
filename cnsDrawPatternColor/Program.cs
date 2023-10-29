namespace cnsDrawPatternColor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            string[] colors = new string[6] { "DarkGreen", "White", "Green", "Blue", "Red", "DarkBlue"};
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < 10; i++)
            {
                Console.Write(" ");
                Console.BackgroundColor = (ConsoleColor)random.Next(1, 16);
            }
        }
    }
}

//Console.ForegroundColor = ConsoleColor.Red;
//Console.BackgroundColor = ConsoleColor.White;
//Console.WriteLine("Красный текст на белом фоне");
//Console.ResetColor();
//Console.WriteLine("Обычный текст");

//foreach (var color in Enum.GetValues<ConsoleColor>())
//{
//    Console.WriteLine(color);
//}