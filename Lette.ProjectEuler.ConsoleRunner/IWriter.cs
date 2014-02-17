using System.Collections.Generic;
using System.Diagnostics;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public interface IWriter
    {
        void PrintHeader();
        void PrintNumberOfFoundSolutions(int count);
        void PrintProgress(string text);
        void NewLine();
        void PrintSummary(Stopwatch stopwatch, int[] results, double totalMilliseconds);
        void PrintLog(List<string> logList);
        void PrintSyntax();
    }
}