﻿
namespace AOC2024.Day3
{
    internal class Day3 : IDay
    {
        public string Name => "Day 3: Mull It Over";
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

        public Day3 (bool debug, string computerPath)
        {
            Debug = debug;
            ComputerPath = ComputerPath;
            PartOne = new Day3Part1(debug, Path);
            PartTwo = new Day3Part2(debug, Path);
        }
    }
}
