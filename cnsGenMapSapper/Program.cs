namespace cnsGenMapSapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintMinefield(GenerateMinefield(9, 9, 10));
        }

        static char[,] GenerateMinefield(int height, int width, int numMines)
        {
            char[,] minefield = new char[height, width];


            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    minefield[row, col] = '0';
                }
            }


            Random random = new Random();
            for (int minesPlaced = 0; minesPlaced < numMines;)
            {
                int randomRow = random.Next(0, height);
                int randomCol = random.Next(0, width);

                if (minefield[randomRow, randomCol] != '*')
                {
                    minefield[randomRow, randomCol] = '*';
                    minesPlaced++;
                }
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (minefield[row, col] == '*')
                        continue;

                    int mineCount = CountAdjacentMines(minefield, row, col, height, width);
                    minefield[row, col] = mineCount.ToString()[0];
                }
            }

            return minefield;
        }

        static int CountAdjacentMines(char[,] minefield, int row, int col, int height, int width)
        {
            int mineCount = 0;
            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    int newRow = row + r;
                    int newCol = col + c;
                    if (newRow >= 0 && newRow < height && newCol >= 0 && newCol < width)
                    {
                        if (minefield[newRow, newCol] == '*')
                        {
                            mineCount++;
                        }
                    }
                }
            }
            return mineCount;
        }

        static void PrintMinefield(char[,] minefield)
        {
            int height = minefield.GetLength(0);
            int width = minefield.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Console.Write(minefield[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}