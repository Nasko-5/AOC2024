namespace AOC2024.Day4
{
    internal class Day5 : IDay
    {
        public string Name => "Day 5: Print Queue";
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
            PartOne.Solve();
            try
            {
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

        public Day5 (bool debug, string computerPath)
        {
            Debug = debug;
            ComputerPath = ComputerPath;
            PartOne = new Day5Part1(debug, Path);
            PartTwo = new Day5Part2(debug, Path);
        }
    }
}
