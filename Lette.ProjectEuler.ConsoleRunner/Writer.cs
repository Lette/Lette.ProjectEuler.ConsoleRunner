using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Lette.ProjectEuler.Core;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class Writer : IWriter
    {
        private readonly TextWriter _writer;

        public Writer(TextWriter writer)
        {
            _writer = writer;
        }

        public void PrintHeader()
        {
            _writer.WriteLine("Project Euler Console App Runner");
        }

        public void PrintNumberOfFoundSolutions(int count)
        {
            _writer.Write("Found {0} solutions ", count);
        }

        public void PrintProgress(string text)
        {
            _writer.Write(text);
        }

        public void NewLine()
        {
            _writer.WriteLine();
        }

        public void PrintSummary(Stopwatch stopwatch, int[] results, double totalMilliseconds)
        {
            NewLine();

            _writer.WriteLine(
                "{0} passed, {1} failed, {2} inconclusive, {3} canceled, {4} faulted.",
                results[SolutionResult.Pass.Key],
                results[SolutionResult.Fail.Key],
                results[SolutionResult.Inconclusive.Key],
                results[SolutionResult.Canceled.Key],
                results[SolutionResult.Faulted.Key]);

            NewLine();

            _writer.WriteLine("Total time     (ms): {0}", (int)stopwatch.Elapsed.TotalMilliseconds);
            _writer.WriteLine("Total cpu time (ms): {0}", (int)totalMilliseconds);
        }

        public void PrintLog(List<string> logList)
        {
            NewLine();
            NewLine();

            foreach (var logMessage in logList)
            {
                _writer.WriteLine(logMessage);
            }
        }

        public void PrintSyntax()
        {
            NewLine();

            _writer.WriteLine(@"Syntax:

    EulerRunner options
    
Options:

    [-p+ | --para[llel]]
        enables parallel execution (default)
        parallelism is limitied by number of available processors/cores

    [-p- | --seq[uential]]
        disables parallel execution
        no parallelism, solutions are not run in parallel

    [{-f | --filter} list]
        filters specific solutions by problem id
        list is a comma- and hyphen-separated list of problem ids

    [-r | --run] file-name
        path to a file containing solutions

Examples

    EulerRunner solutions.dll
        executes all solutions in solutions.dll in parallel

    EulerRunner solutions.dll -f 70,75-79 --seq
        executes solutions with problem id 70, 75, 76, 77, 78 and 79 (if found) in sequence

    EulerRunner --parallel --filter -10 solutions.dll
        executes all solutions in solutions.dll with problem id up to 10 in parallel");

            NewLine();
        }
    }
}