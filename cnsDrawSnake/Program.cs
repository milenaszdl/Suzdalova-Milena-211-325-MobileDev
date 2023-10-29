namespace cnsDrawSnake
{
    internal class Program
    {
        static void Main( )
        {
            Console.Write("Введите направление змейки (1 - по часовой стрелке, 2 - против часовой стрелки): ");
            int direction = Convert.ToInt32(Console.ReadLine());

            Console.Write("Выберите ориентацию змейки (1 - вертикальная, 2 - горизонтальная): ");
            int orientation = Convert.ToInt32(Console.ReadLine());
            int width = 10; // Ширина поля
            int height = 10; // Высота поля

            if (orientation == 1)
            {
                DrawSnakeVertical(width, height, direction);
            }
            else if (orientation == 2)
            {
                DrawSnakeHorizontal(width, height, direction);
            }
            else
            {
                Console.WriteLine("Неверный выбор ориентации.");
            }

            Console.Write("Повторить? (y/n):");
            string? repeat = Console.ReadLine();
            if (repeat == "y")
            {
                Main( );
            }
        }

        static void DrawSnakeVertical(int width, int height, int direction)
        {
            int count = 0; //счетчик -  четную или нечетную строку мы рисуем
            for (int i = 0; i < height; i++)
            {
                if (i % 2 == 1)
                {
                    count++;
                    if (direction == 1)
                    {
                        if (count % 2 == 1)
                        {
                            Console.CursorLeft = width - 1;
                            Console.Write("█");
                        }
                        else
                        {
                            Console.Write("█");
                        }
                    }
                    else if (direction == 2)
                    {
                        if (count % 2 == 0)
                        {
                            Console.CursorLeft = width - 1;
                            Console.Write("█");
                        }
                        else
                        {
                            Console.Write("█");
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write("█");
                    }
                }
                Console.WriteLine();
            }
        }

        static void DrawSnakeHorizontal(int width, int height, int direction)
        {
            for (int i = 0; i < height; i++)
            {
                int count = 0;
                for (int j = 0; j < width; j++)
                {
                    if (j % 2 == 0)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        count++;
                        if (direction == 1)
                        {
                            if (i == 0)
                            {
                                if (count % 2 == 1)
                                {
                                    Console.Write("█");
                                }
                                else
                                {
                                    Console.Write(' ');
                                }

                            }
                            else if (i == height - 1)
                            {
                                if (count % 2 == 0)
                                {
                                    Console.Write("█");
                                }
                                else
                                {
                                    Console.Write(' ');
                                }
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                if (count % 2 == 0)
                                {
                                    Console.Write("█");
                                }
                                else
                                {
                                    Console.Write(' ');
                                }

                            }
                            else if (i == height - 1)
                            {
                                if (count % 2 == 1)
                                {
                                    Console.Write("█");
                                }
                                else
                                {
                                    Console.Write(' ');
                                }
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }

                    }
                }

                Console.WriteLine();
            }
        }
    }
}
