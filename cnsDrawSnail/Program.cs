namespace cnsDrawSnail
{
    using System; 

    internal class Program
    {
        static void Main(string[] args)
        {
            DrawSpiral(10, 10, Direction.Clockwise);
        }
        static void DrawSpiral(int width, int height, Direction direction)
        {
            int[,] spiral = new int[width, height];
            int number = 1;
            int x = 0;
            int y = 0;
            int dx = 0;
            int dy = 0;

            switch (direction)
            {
                case Direction.Clockwise:
                    dx = 1;
                    break;
                case Direction.CounterClockwise:
                    dy = 1;
                    break;
            }
            while (number <= width * height)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    spiral[x, y] = number;
                    number++;
                }

                if (x + dx >= width || x + dx < 0 || y + dy >= height || y + dy < 0 || spiral[x + dx, y + dy] != 0)
                {
                    switch (direction)
                    {
                        case Direction.Clockwise:
                            RotateClockwise(ref dx, ref dy);
                            break;
                        case Direction.CounterClockwise:
                            RotateCounterClockwise(ref dx, ref dy);
                            break;
                    }
                }

                x += dx;
                y += dy;
            }

            Console.BackgroundColor = ConsoleColor.Green;
            // Выводим спираль в консоль
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write($"{spiral[j, i],-3}");
                }
                Console.WriteLine();
            }
        }

        static void RotateClockwise(ref int dx, ref int dy)
        {
            int temp = dx;
            dx = dy;
            dy = -temp;
        }

        static void RotateCounterClockwise(ref int dx, ref int dy)
        {
            int temp = dx;
            dx = -dy;
            dy = temp;
        }
    }
}
enum Direction
{
    Clockwise,
    CounterClockwise
}
