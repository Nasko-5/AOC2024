using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024.Day1
{
    internal class Day1Part2 : IPart
    {
        public object Parse()
        {
            List<string> lists = RawInput
                .Replace("\r", "")
                .Split('\n')
                .ToList();

            List<int> listOne = new List<int>();
            List<int> listTwo = new List<int>();

            foreach (string pair in lists)
            {
                List<int> ids = pair
                    .Split(new string[] { "   " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                listOne.Add(ids[0]);
                listTwo.Add(ids[1]);
            }

            List<List<int>> parsedLists = new List<List<int>> { listOne, listTwo };
            return parsedLists;

        }

        public void Solve()
        {
            List<List<int>> input = (List<List<int>>)Parse();

            Console.WriteLine("Initial:");
            Console.WriteLine("    " + string.Join(" ", input[0]));
            Console.WriteLine("    " + string.Join(" ", input[1]));

            Dictionary<int, int> values = new Dictionary<int, int>();

            foreach (int id in input[0])
            {
                int count = input[1].Count(a => a == id);
                if (values.ContainsKey(id)) values[id] += count;
                else values[id] = count;
            }

            int sum = 0;
            Console.WriteLine("\nOccurances and products:");
            foreach (KeyValuePair<int, int> entry in values)
            {
                int res = entry.Key * entry.Value;
                sum += res;
                Console.WriteLine($"    {entry.Key} appears {entry.Value} times ({entry.Key} * {entry.Value} = {res})");
            }

            GotAnswer = sum;

            Console.WriteLine($"\nResult = {GotAnswer}");
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 18567089;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText(Path+(Debug ? "testInput.txt" : "realInput.txt")); } }
        public IDay PartOfDay { get; set; }
        public Day1Part2(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
