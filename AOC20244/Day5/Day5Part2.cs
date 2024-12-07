using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC2024.Day4
{
    internal class Day5Part2 : IPart
    {
        public object Parse()
        {
            return 0;
        }

        public void Solve()
        {
            
        }
        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 6470;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText(Path+(Debug ? "testInput.txt" : "realInput.txt")); } }
        public IDay PartOfDay { get; set; }
        public Day5Part2(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
