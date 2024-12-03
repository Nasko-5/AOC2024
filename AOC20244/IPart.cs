using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024
{
    public interface IPart
    {
        int CorrectAnswer { get; }
        int GotAnswer { get; set;  } 
        bool Solved { get; }
        bool Debug { get; set; }
        string Path { get; set; }
        string RawInput { get; }
        IDay PartOfDay { get; set; }

        object Parse();
        void Solve();
    }
}
