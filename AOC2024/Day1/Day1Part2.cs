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
            throw new NotImplementedException();
        }

        public void Solve()
        {
            throw new NotImplementedException();
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText(Path + (Debug ? "testInput.txt" : "realInput.txt")); } }
        public IDay PartOfDay { get; set; }
        public int CorrectAnswer => int.MinValue;
        public int GotAnswer { get; set; }
        public Day1Part2(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
