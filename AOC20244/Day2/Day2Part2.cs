using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024.Day2
{
    internal class Day2Part2 : IPart
    {
        public object Parse()
        {
            var reports = RawInput
                .Replace("\r", "")
                .Split('\n')
                .Select(a => a.Split(' ').Select(int.Parse).ToList())
                .ToArray();

            return reports;
        }

        public void Solve()
        {
            List<int>[] reports = (List<int>[])Parse();
            int safeReports = 0;

            Console.WriteLine();
            for (int reportIndex = 0; reportIndex < reports.Length; reportIndex++)
            {
                List<int> tempReport = reports[reportIndex];

                Console.WriteLine($"Testing report {reportIndex+1}/{reports.Length} ({string.Join(" ", tempReport)})");

                Console.Write($"   Is safe as is?");
                if (reportIsSafe(tempReport))
                {
                    Console.WriteLine(" | Safe!");
                    safeReports++;
                    Console.WriteLine($"Safe reports so far: {safeReports}");
                    if (Debug) Console.ReadLine();
                    continue;
                }

                Console.WriteLine(" | Not Safe.");

                for (int level = 0; level < tempReport.Count+1; level++)
                {
                    string levelString = $"{level}{(level == 1 ? "st" : level == 2 ? "nd" : level == 3 ? "rd" : "th")}";
                    tempReport = new List<int>(reports[reportIndex]);
                    tempReport.RemoveAt(level);
                    Console.Write($"   Removing the {levelString} level -> ({string.Join(" ", tempReport)})");
                    if (reportIsSafe(tempReport))
                    {
                        Console.WriteLine(" | Safe!");
                        safeReports++;
                        break;
                    } else
                    {
                        Console.WriteLine(" | Not Safe.");
                    }
                }
               
                Console.WriteLine($"Safe reports so far: {safeReports}");
                if(Debug)Console.ReadLine();
            }

            Console.WriteLine($"\nFinal safe reports: {safeReports}");

            GotAnswer = safeReports;
        }


        private bool reportIsSafe(List<int> report)
        {
            int delta = 0;
            int sign = 0;
            int oldSign = Math.Sign(report[0] - report[1]);
            bool isSafe = false;

            Console.Write(" | ");
            for (int level = 0; level < report.Count - 1; level++)
            {
                delta = report[level] - report[level + 1];
                sign = Math.Sign(delta);
                delta = Math.Abs(delta);
                bool inRange = delta < 1 || delta > 3;
                bool directionChanged = sign != oldSign;

                Console.Write($"{delta}{(!inRange ? "*" : "-")}{(sign == 1 ? "v" : "^")} ");
                if (inRange || directionChanged)
                {
                    isSafe = false;
                    break;
                }
                isSafe = true;

                oldSign = sign;
            }
            //Console.WriteLine();
            return isSafe;
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 337;
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
