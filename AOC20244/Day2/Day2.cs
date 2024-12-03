using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024.Day2
{
    internal class Day2 : IDay
    {
        public string Name => "Day 2: Red-Nosed Reports";
        public int Stars { get { return (PartOne.Solved ? 1 : 0) + (PartTwo.Solved ? 1 : 0); } }
        public bool Completed { get { return Stars == 2; } }
        public string Path { get { return $@"{ComputerPath}{this.GetType().Name}/";  } }
        public string ComputerPath { get; set; }
        public IPart PartOne { get; set; }
        public IPart PartTwo { get; set; }
        public bool Debug { get; set; }

        public void RunPartOne()
        {
            string sep = new string('*', Console.WindowWidth);
            try
            {
                PartOne.Solve();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Part 1 of {Name} Failed!\n{sep}\n{e.Message}\n{sep}\nStacktrace:\n{e.StackTrace}\n{sep}");
            }
        }

        public void RunPartTwo()
        {
            string sep = new string('*', Console.WindowWidth);
            try
            {
                PartTwo.Solve();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Part 2 of {Name} Failed!\n{sep}\n{e.Message}\n{sep}\nStacktrace:\n{e.StackTrace}\n{sep}");
            }
        }

        public Day2 (bool debug, string computerPath)
        {
            Debug = debug;
            ComputerPath = ComputerPath;
            PartOne = new Day2Part1(debug, Path);
            PartTwo = new Day2Part2(debug, Path);
        }
    }
}
