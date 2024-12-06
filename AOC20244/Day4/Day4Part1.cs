
using System.Globalization;

namespace AOC2024.Day4
{
    internal class Day4Part1 : IPart
    {
        public object Parse()
        {
            char[][] parsed = RawInput
                .Replace("\r", "")
                .Split("\n")
                .Select(a => a.ToCharArray())
                .ToArray();

            return GridHelper<char>.To2D(parsed);
        }

        public void Solve()
        {
            char[,] m = (char[,])Parse();
            char[,] neighbors;

            int count = 0;
            Console.WriteLine();
            for (int y = 0; y < m.GetLength(0); y++)
            {
                for (int x = 0; x < m.GetLength(1); x++)
                {
                    char at = m[y,x];
                    if (at == 'S')
                    {
                        //neighbors = GridHelper<char>.GetNeighbors(m, x, y, 4);
                        Console.WriteLine($"Checking ({x},{y}):");
                        Console.WriteLine($"   left:    {string.Join(" ", GridHelper<char>.GetRow(m, y, x - 3, x + 1))}");
                        Console.WriteLine($"   right:   {string.Join(" ", GridHelper<char>.GetRow(m, y, x, x + 4))}");
                        Console.WriteLine($"   top:     {string.Join(" ", GridHelper<char>.GetColumn(m, x, y - 3, y + 1))}");
                        Console.WriteLine($"   bottom:  {string.Join(" ", GridHelper<char>.GetColumn(m, x, y, y + 4))}");
                        Console.WriteLine($"   lrb dia: {string.Join(" ", GridHelper<char>.GetDiagonalBLR1D(m, x, y, 4))}");
                        Console.WriteLine($"   rlb dia: {string.Join(" ", GridHelper<char>.GetDiagonalBRL1D(m, x, y, 4))}");
                        Console.WriteLine($"   lrf dia: {string.Join(" ", GridHelper<char>.GetDiagonalFRL1D(m, x, y, 4))}");
                        Console.WriteLine($"   rlf dia: {string.Join(" ", GridHelper<char>.GetDiagonalFLR1D(m, x, y, 4))}");

                    }
                }
            }

            
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 0;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText($@"{Path}{(Debug ? "testInput.txt" : "realInput.txt")}"); } }
        public IDay PartOfDay { get; set; }
        public Day4Part1(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
