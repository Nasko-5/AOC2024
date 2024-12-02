using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024.Day2
{
    internal class Day2Part1 : IPart
    {
        public object Parse()
        {
            var reports = RawInput
                .Replace("\r", "")
                .Split('\n')
                .Select(a => a.Split(' ').Select(int.Parse).ToArray())
                .ToArray();

            return reports;
        }

        public void Solve()
        {
            int[][] reports = (int[][])Parse();
            bool[] reportSafety = new bool[reports.Length];

            Console.WriteLine();
            for (int reportIndex = 0; reportIndex < reports.Length; reportIndex++)
            {
                int[] report = reports[reportIndex];

                int delta = 0;
                int sign = 0;
                int oldSign = Math.Sign(report[0] - report[1]);

                Console.WriteLine($"Report {reportIndex} ({string.Join(" ", report)})");
                for (int i = 0; i < report.Length - 1; i++)
                {
                    delta = report[i] - report[i + 1];
                    sign = Math.Sign(delta);
                    delta = Math.Abs(delta);

                    Console.Write($"   delta = {delta} | ");
                    if (delta < 1 || delta > 3 || sign != oldSign)
                    {
                        reportSafety[reportIndex] = false;
                        Console.WriteLine($"Unsafe | Delta is less than one or greater than three, and it is not linear");
                        break;
                    }
                    else reportSafety[reportIndex] = true;
                    Console.WriteLine(    $"Safe   | Delta is greater than one and less than three, and it is linear");

                    oldSign = sign;

                }
                //Console.Write($"{(reportSafety[reportIndex] ? "Safe  " : "Unsafe")} | {string.Join(" ", report)}\r");
                Console.WriteLine();
            }

            int safeReports = reportSafety.Count(a => a == true);
            Console.WriteLine($"\nSafe reports: {safeReports}");

            GotAnswer = safeReports;
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 269;
        public int GotAnswer { get; set; }
        public bool Debug { get; set; }
        public string Path { get; set; }
        public string RawInput { get { return File.ReadAllText($@"{Path}{(Debug ? "testInput.txt" : "realInput.txt")}"); } }
        public IDay PartOfDay { get; set; }
        public Day2Part1(bool debug, string path)
        {
            Debug = debug;
            Path = path;
        }
    }
}
