
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
            Console.WriteLine("I got burtn out :(");
        //    char[,] m = (char[,])Parse();
        //    char[,] neighbors;

        //    int count = 0;
        //    string xmas = "XMAS";
        //    string samx = "SAMX";

        //    Console.WriteLine();

        //    for (int y = 0; y < m.GetLength(0); y++)
        //    {
        //        for (int x = 0; x < m.GetLength(1); x++)
        //        {
        //            char at = m[y, x];
        //            if (at == 'S')
        //            {
        //                string left = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y, x - 1)}{GridHelper<char>.Index(m, y, x - 2)}{GridHelper<char>.Index(m, y, x - 3)}";
        //                string right = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y, x + 1)}{GridHelper<char>.Index(m, y, x + 2)}{GridHelper<char>.Index(m, y, x + 3)}";
        //                string top = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y + 1, x)}{GridHelper<char>.Index(m, y + 2, x)}{GridHelper<char>.Index(m, y + 3, x)}";
        //                string bottom = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y - 1, x)}{GridHelper<char>.Index(m, y - 2, x)}{GridHelper<char>.Index(m, y - 3, x)}";
        //                string rlbDia = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y - 1, x - 1)}{GridHelper<char>.Index(m, y - 2, x - 2)}{GridHelper<char>.Index(m, y - 3, x - 3)}";
        //                string lrbDia = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y + 1, x + 1)}{GridHelper<char>.Index(m, y + 2, x + 2)}{GridHelper<char>.Index(m, y + 3, x + 3)}";
        //                string lrfDia = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y + 1, x - 1)}{GridHelper<char>.Index(m, y + 2, x - 2)}{GridHelper<char>.Index(m, y + 3, x - 3)}";
        //                string rlfDia = $"{GridHelper<char>.Index(m, y, x)}{GridHelper<char>.Index(m, y + 1, x + 1)}{GridHelper<char>.Index(m, y + 2, x + 2)}{GridHelper<char>.Index(m, y + 3, x + 3)}";

        //                var matchlist = new string[] { left, right, top, bottom, rlbDia, lrbDia, lrfDia, rlfDia };
        //                int valid = matchlist.Count(a => a == xmas || a == samx);

        //                //neighbors = GridHelper<char>.GetNeighbors(m, x, y, 4);
        //                Console.WriteLine($"Checking ({x},{y}):");
        //                Console.WriteLine($"   left:    {left}");
        //                Console.WriteLine($"   right:   {right}");
        //                Console.WriteLine($"   top:     {top}");
        //                Console.WriteLine($"   bottom:  {bottom}");
        //                Console.WriteLine($"   rlb dia: {rlbDia}");
        //                Console.WriteLine($"   lrb dia: {lrbDia}");
        //                Console.WriteLine($"   lrf dia: {lrfDia}");
        //                Console.WriteLine($"   rlf dia: {rlfDia}");
        //                Console.WriteLine($"   valid: {valid}");
        //                count += valid;
        //            }
        //        }
        //    }

        //    Console.WriteLine(count);
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 4;
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
