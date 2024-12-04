using System.Text.RegularExpressions;

namespace AOC2024.Day3
{
    internal class Day3Part1 : IPart
    {
        public object Parse()
        {
            Regex mulRegex = new(@"mul\((\d{1,3},\d{1,3})\)");

            var matches = mulRegex.Matches(RawInput);
            (int, int)[] parsed = new (int, int)[matches.Count];

            for (int match = 0; match < matches.Count; match++)
            {
                var toMult = matches[match].Groups[1].Value
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

                parsed[match] = (toMult[0], toMult[1]);
            }

            return parsed;
        }

        public void Solve()
        {
            (int, int)[] parsed = ((int, int)[])Parse();
            Console.WriteLine($"\nExtracted:\n   {string.Join(" ", parsed.Select(a => $"mul({a.Item1},{a.Item2})").ToArray())}\n");

            Console.WriteLine($"Calculate:\n   {string.Join("+", parsed.Select(a => $"({a.Item1}*{a.Item2})").ToArray())}\n");
            int sum = 0;
            foreach (var pair in  parsed)
            {
                sum += pair.Item1 * pair.Item2;
            }
            Console.WriteLine($"Answer = {sum}\n");
            GotAnswer = sum;
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 269;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText($@"{Path}{(Debug ? "testInput.txt" : "realInput.txt")}"); } }
        public IDay PartOfDay { get; set; }
        public Day3Part1(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
