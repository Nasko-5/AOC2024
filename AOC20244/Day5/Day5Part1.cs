
using System.Globalization;
using System.Linq;

namespace AOC2024.Day4
{
    internal class Day5Part1 : IPart
    {
        public object Parse()
        {
            var splitInput = RawInput
                .Replace("\r", "")
                .Split("\n\n");

            int[][] rules = splitInput[0]
                .Split('\n')
                .Select(a => a.Split('|').Select(int.Parse).ToArray())
                .ToArray();

            int[][] updates = splitInput[1]
                .Split('\n')
                .Select(a => a.Split(',').Select(int.Parse).ToArray())
                .ToArray();

            (int[][] rules, int[][] pages) parsed = (rules, updates);
            return parsed;
        }

        public void Solve()
        {
            (int[][] rules, int[][] updates) parsed = ((int[][] rules, int[][] updates))Parse();

            var validUpdates = parsed.updates.Where(update => verifyUpdate(parsed.rules, update));


            Console.WriteLine("\nValid Updates: \n" + string.Join("\n", validUpdates.Select(a => "   " + string.Join(" ", a))));

            int midSum = validUpdates.Sum(a => a[(int)Math.Floor((double)a.Length / 2)]);

            Console.WriteLine($"\nSum of middle numbers: {midSum}\n");
        }

        bool verifyUpdate(int[][] rules, int[] update)
        {
            var relevantRules = rules.Where(a => update.Contains(a[0]) && update.Contains(a[1])).ToArray();

            foreach (int page in update)
            {
                var relevantRulesForUpdate = relevantRules.Where(a => a[0] == page);

                foreach (var relevantRule in relevantRulesForUpdate)
                {
                    int xPos = Array.IndexOf(update, page);
                    int yPos = Array.IndexOf(update, relevantRule[1]);

                    if (xPos >= yPos) return false;
                }
            }
            return true;
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 7198;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText($@"{Path}{(Debug ? "testInput.txt" : "realInput.txt")}"); } }
        public IDay PartOfDay { get; set; }
        public Day5Part1(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
