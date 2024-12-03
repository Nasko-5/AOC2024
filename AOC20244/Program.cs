using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024
{
    public class Program
    {
        public static bool debug = true;
        public static string computerPath = @"D:/My stuff/.programming/csharp/AOC2024/AOC2024/"; // make sure it ends with a slash
        public static void Main()
        {
            var days = GetDays();
            string uin = "";
            string err = "";

            while (true)
            {
                Console.Clear();
                printBanner();

                Console.WriteLine("Day listing:");
                foreach (var day in days)
                {
                    Console.WriteLine($"\t{day.Name}{new string(' ', 30-day.Name.Length)}| {(day.PartOne.Solved ? "*" : "-")} {(day.PartTwo.Solved ? "*" : "-")}");
                }

                Console.Write($"\n{(err != "" ? $"Input Error: {err}!\n" : "")}Select Day: ");

                uin = Console.ReadLine();
                if (!string.IsNullOrEmpty(uin) && !uin.All(x => char.IsDigit(x))) { err = $"{uin} is not a number"; continue; }

                int dayNumber = int.Parse(uin);
                if (dayNumber < 1 || dayNumber > days.Count) { err = $"Day {dayNumber} is out of range"; continue; }

                Console.Clear();
                printBanner();

                IDay selectedDay = days[dayNumber - 1];
                selectedDay.ComputerPath = computerPath;

                Console.Write("Debug? (Y/n): ");
                uin = Console.ReadLine().ToUpper();
                if (uin == "Y")
                {
                    selectedDay.Debug = true;
                    selectedDay.PartOne.Debug = true;
                    selectedDay.PartTwo.Debug = true;
                }
                else
                {
                    selectedDay.Debug = false;
                    selectedDay.PartOne.Debug = false;
                    selectedDay.PartTwo.Debug = false;
                }

                Console.Write("Run part (1/2): ");
                uin = Console.ReadLine();
                if (!string.IsNullOrEmpty(uin) && !uin.All(x => char.IsDigit(x))) { err = $"{uin} is not a number"; continue; }

                int partNumber = 0; 

                // this is hacky but im impatient and i wanna start solving already
                try { partNumber = int.Parse(uin); }
                catch { err = $"Bad Input"; continue; }

                if (partNumber == 1)
                {
                    selectedDay.PartOne.Path = selectedDay.Path;
                    selectedDay.RunPartOne();
                }
                if (partNumber == 2)
                {
                    selectedDay.PartTwo.Path = selectedDay.Path;
                    selectedDay.RunPartTwo();
                }
                

                Console.Write("Press any key to continue...");
                Console.ReadLine();
            }
        }
        public static List<IDay> GetDays()
        {
            // Dynamically find all classes inheriting from `Day`
            // Get all types in the current assembly that implement IDay
            var dayImplementations = Assembly.GetExecutingAssembly()
                                             .GetTypes()
                                             .Where(t => typeof(IDay).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                                             .ToList();

            // Convert to a list of instances (if needed)
            var dayInstances = dayImplementations
            .Select(type =>
            {
                var constructor = type.GetConstructors().FirstOrDefault();
                if (constructor != null)
                {
                    return (IDay)constructor.Invoke(new object[] { debug, computerPath });
                }
                return null;
            })
            .Where(instance => instance != null)
            .ToList();

            dayInstances = dayInstances.OrderBy(a => a.Name).ToList();

            return dayInstances;
        }
        public static void printBanner()
        {
            Console.Write("┌─────────────────────────────────────────────────────────────────┐\n");

            Console.Write("│    ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("a     "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("oOOOo   "); Console.ResetColor();
            Console.Write(",CCCc     ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("o"); Console.ResetColor();
            Console.Write("    ,22222,  o0O0o  ,22222,   ,444  │\n");

            // "│   aAa   O°   °O cC   ``  oO0Oo  22   22 oO   Oo 22   22  44 44  │\n"

            Console.Write("│   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("a^a   "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O°   °O "); Console.ResetColor();
            Console.Write("cC   ``  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("oO0Oo"); Console.ResetColor();
            Console.Write("  22` ;22 oO   Oo 22` ;22  44 44  │\n");

            //"|  aAaAa  Oo   oO cC   ,,   O°O      22   oO   Oo    22   4444444 │\n"

            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("aAaAa  "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Oo   oO "); Console.ResetColor();
            Console.Write("cC   ,,   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("O°O"); Console.ResetColor();
            Console.Write("     ,22;  oO   Oo   ,22;  4444444 │\n");

            //"│ aA   Aa  °OOO°   ^CCC°          2222222  °0O0°  2222222     44  │\n"

            Console.Write("│ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("aA   Aa  "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("°OOO°   "); Console.ResetColor();
            Console.Write("^CCC°          ");
            Console.Write("2222222  °0O0°  2222222     44  │\n");

            Console.WriteLine("└─────────────────────────────────────────────────────────────────┘\n");
        }
    }
}
