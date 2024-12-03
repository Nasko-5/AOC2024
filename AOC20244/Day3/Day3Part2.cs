using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC2024.Day2
{
    internal class Day3Part2 : IPart
    {
        public object Parse()
        {
            string commandRegex = @"mul\(\d{1,3},\d{1,3}\)|don[']t\(\)|do\(\)";
            var matches = Regex.Matches(RawInput, commandRegex, RegexOptions.None);

            (int, int)[] parsed = new (int, int)[matches.Count];

            for (int match = 0; match < parsed.Length; match++)
            {
                string stringMatch = matches[match].Value;
                if (stringMatch.Contains("don't")) { parsed[match] = (int.MinValue, int.MinValue); continue; }
                else if (stringMatch.Contains("do")) { parsed[match] = (int.MaxValue, int.MaxValue); continue; }
                
                var parsedMatch = stringMatch
                    .Replace("mul", "")
                    .Replace("(","").Replace(")","")
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
                
                parsed[match] = (parsedMatch[0], parsedMatch[1]);
            }

            return parsed;
        }

        public void Solve()
        {
            (int, int)[] parsed = ((int, int)[])Parse();

            Console.WriteLine($"\nExtracted:\n   {string.Join(' ', parsed.Select(a => $"{(a.Item1 == int.MaxValue ? "do()" : a.Item1 == int.MinValue ? "don't()" : $"mul({a.Item1},{a.Item2})")}"))}");

            bool instructionsEnabled = true;
            int sum = 0;

            Console.Write($"\nCalculation:\n   ");
            foreach (var instruction in parsed)
            {
                if (instruction.Item1 == int.MaxValue) { instructionsEnabled = true; continue; }
                else if (instruction.Item1 == int.MinValue) { instructionsEnabled = false; continue; }
                if (instructionsEnabled)
                {
                    Console.Write($"({instruction.Item1}*{instruction.Item2}) + ");
                    sum += instruction.Item1 * instruction.Item2;
                }
            }
            Console.WriteLine("0");

            Console.WriteLine($"\nAnswer = {sum}\n");
            GotAnswer = sum;
        }




        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 72948684;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText(Path+(Debug ? "testInput.txt" : "realInput.txt")); } }
        public IDay PartOfDay { get; set; }
        public Day3Part2(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
