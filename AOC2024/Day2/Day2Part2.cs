using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024.Day2
{
    internal class Day2Part2 : IPart
    {
        public object Parse()
        {
            return RawInput;
        }

        public void Solve()
        {
            Console.WriteLine(Parse());
            GotAnswer = 0;
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 18567089;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText(Path+(Debug ? "testInput.txt" : "realInput.txt")); } }
        public IDay PartOfDay { get; set; }
        public Day2Part2(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
