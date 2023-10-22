namespace cnsOOPStudent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student x = new();
            x.AgeCreated += X_AgeCreated;
            x.AgeCreatedInt += X_AgeCreatedInt;
            x.Name = "Иван";
            x.Surname = "Иванов";
            x.Age = 19;
            Console.WriteLine(x.GetFullName());
            x.Age = -3;
            Console.WriteLine(x.GetFullName());

            Student x2 = new()
            {
                Name = "Петя",
                Surname = "Алякимов",
                Age = 19
            };
            x2.AgeCreated += X_AgeCreated;
            Console.WriteLine(x2.GetFullName());
        }

        private static void X_AgeCreatedInt(object? sender, EventArgs e, int new_age)
        {
            Console.WriteLine($"X_AgeCreatedInt => {new_age}");
        }

        private static void X_AgeCreated(object? sender, EventArgs e)
        {
            if (sender is Student student)
            {
                Console.WriteLine("изменился возраст," +
                $" новый возраст равен = {student.Age}");
            }
        }
    }
}