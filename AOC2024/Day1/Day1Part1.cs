using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024.Day1
{
    internal class Day1Part1 : IPart
    {
        public object Parse()
        {
            List<string> lists = RawInput
                .Split('\n')
                .ToList();

            List<int> listOne = new List<int>();
            List<int> listTwo = new List<int>();

            foreach (string pair in lists)
            {
                List<int> ids = pair
                    .Split(new string[] { "   " }, StringSplitOptions.None)
                    .Select(int.Parse)
                    .ToList();
                listOne.Add(ids[0]);
                listTwo.Add(ids[1]);
            }

            List <List<int>> parsedLists = new List<List<int>> { listOne, listTwo };
            return parsedLists;

        }

        public void Solve()
        {
            List<List<int>> input = (List<List<int>>)Parse();

            Console.WriteLine("Initial:");
            Console.WriteLine("\t"+string.Join(" ", input[0]));
            Console.WriteLine("\t"+string.Join(" ", input[1]));

            input[0] = input[0].OrderBy(a => a).ToList();
            input[1] = input[1].OrderBy(a => a).ToList();

            Console.WriteLine("\nSorted:");
            Console.WriteLine("\t" + string.Join(" ", input[0]));
            Console.WriteLine("\t" + string.Join(" ", input[1]));

            List<int> distances = new List<int>();
            for (int i = 0; i < input[0].Count; i++)
            {
                distances.Add(Math.Abs(input[0][i] - input[1][i]));
            }

            GotAnswer = distances.Sum();

            Console.WriteLine($"\nDistances:\n\t{string.Join(" + ", distances)}\n\nAnswer = {GotAnswer}\n");
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => int.MinValue;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText(Path + (Debug ? "testInput.txt" : "realInput.txt")); } }
        public IDay PartOfDay { get; set; }
        public Day1Part1(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
