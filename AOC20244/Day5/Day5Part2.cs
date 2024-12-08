using System.Diagnostics.Tracing;
using System.Formats.Asn1;
using System.IO;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC2024.Day4
{
    internal class Day5Part2 : IPart
    {
        public object Parse()
        {
            var splitInput = RawInput
                .Replace("\r", "")
                .Split("\n\n");

            int[][] rules = splitInput[0]
                .Split('\n')
                .Select(a => a.Split('|').Select(int.Parse).ToArray())
                .ToArray();

            int[][] updates = splitInput[1]
                .Split('\n')
                .Select(a => a.Split(',').Select(int.Parse).ToArray())
                .ToArray();

            (int[][] rules, int[][] pages) parsed = (rules, updates);
            return parsed;
        }

        public void Solve()
        {
            (int[][] rules, int[][] updates) parsed = ((int[][] rules, int[][] updates))Parse();

            var invalidUpdates = parsed.updates.Where(update => !VerifyUpdate(parsed.rules, update));
            var correctedUpdates = invalidUpdates.Select(update => fixUpdate(parsed.rules, update)).ToArray();
            int midSum = correctedUpdates.Sum(a => a[(int)Math.Floor((double)a.Length / 2)]);

            Console.WriteLine($"\nSum of middle numbers: {midSum}\n");
        }

        int[] fixUpdate(int[][] rules, int[] update)
        {

            int[] inprogress = new int[update.Length];
            bool breakout = false;
            Array.Copy(update, inprogress, update.Length);
            

            while (!VerifyUpdate(rules, inprogress))
            {
                for (int pageIndex = 0; pageIndex < inprogress.Length && !breakout; pageIndex++)
                {
                    int page = inprogress[pageIndex];
                    int[][] relevantRules = rules.Where(a => a[0] == page && update.Contains(a[1])).ToArray();
                    
                    for (int ruleIndex = 0; ruleIndex < relevantRules.Length && !breakout; ruleIndex++)
                    {
                        int xPos = Array.IndexOf(inprogress, page);
                        int yPos = Array.IndexOf(inprogress, relevantRules[ruleIndex][1]);

                        if (xPos >= yPos)
                        {
                            inprogress = Swap(inprogress, xPos, yPos);
                        }
                    }
                }
            }

            return inprogress;
        }

        int[] Swap(int[] arraya, int indexOne, int indexTwo)
        {
            int valOne = arraya[indexOne], valTwo = arraya[indexTwo];
            arraya[indexTwo] = valOne;
            arraya[indexOne] = valTwo;
            return arraya;
        }


        bool VerifyUpdate(int[][] rules, int[] update)
        {
            var relevantRules = rules.Where(a => update.Contains(a[0]) && update.Contains(a[1])).ToArray();

            foreach (int page in update)
            {
                var relevantRulesForUpdate = relevantRules.Where(a => a[0] == page);

                foreach (var relevantRule in relevantRulesForUpdate)
                {
                    int xPos = Array.IndexOf(update, page);
                    int yPos = Array.IndexOf(update, relevantRule[1]);

                    if (xPos >= yPos) return false;
                }
            }
            return true;
        }

        public bool Solved { get { return CorrectAnswer == GotAnswer; } }
        public int CorrectAnswer => 4230;
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
