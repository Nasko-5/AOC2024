using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024
{
    public interface IDay
    {
        string Name { get; }
        int Stars { get; }
        bool Completed { get; }
        bool Debug { get; set; }
        string Path { get; }
        IPart PartOne { get; }
        IPart PartTwo { get; }

        void RunPartOne();
        void RunPartTwo();
    }
}
