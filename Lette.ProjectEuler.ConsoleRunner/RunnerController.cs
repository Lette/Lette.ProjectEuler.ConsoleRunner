using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Lette.ProjectEuler.Core;
using Lette.ProjectEuler.Core.Runner;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class RunnerController
    {
        private readonly IWriter _writer;
        private readonly IArgumentsParser _argumentsParser;
        private readonly IEnvironmentWrapper _environment;
        private readonly IPredicateBuilder _predicateBuilder;
        private readonly ISolver _solver;
        private readonly IProblemSuiteBuilder _suiteBuilder;

        public RunnerController(
            IWriter writer,
            IArgumentsParser argumentsParser,
            IEnvironmentWrapper environment,
            IPredicateBuilder predicateBuilder,
            ISolver solver,
            IProblemSuiteBuilder suiteBuilder)
        {
            _writer = writer;
            _argumentsParser = argumentsParser;
            _environment = environment;
            _predicateBuilder = predicateBuilder;
            _solver = solver;
            _suiteBuilder = suiteBuilder;
        }

        private double _totalMilliseconds;
        private readonly int[] _results = new int[5];
        private readonly List<string> _logList = new List<string>();

        public void Start()
        {
            _writer.PrintHeader();

            var settings = _argumentsParser.Parse(_environment.GetCommandLineArgs());

            if (settings.FilePath == null)
            {
                _writer.PrintSyntax();
                throw new ArgumentException("Must supply a filepath.");
            }

            if (!File.Exists(settings.FilePath))
            {
                throw new FileNotFoundException("File not found.", settings.FilePath);
            }

            var filterPredicate = _predicateBuilder.CreateFromFilter(settings.Filter);

            var suite = _suiteBuilder
                .CreateFromAssembly(settings.FilePath)
                .Where(p => filterPredicate(p.GetMetaData().Number))
                .ToList();

            _writer.PrintNumberOfFoundSolutions(suite.Count());

            var stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            _solver.SolveAll(suite, Log, settings.Parallel);

            stopwatch.Stop();

            _writer.PrintLog(_logList);
            _writer.PrintSummary(stopwatch, _results, _totalMilliseconds);
        }

        private void Log(Solution solution)
        {
            _totalMilliseconds += solution.ElapsedTime.TotalMilliseconds;

            var result = SolutionResult.Create(solution);

            _results[result.Key]++;

            _writer.PrintProgress(result.ShortDescription);

            var logMessage = string.Format(
                " {4,-5} {0,5} {1,14} {2,14} {3,8} {5}",
                solution.Number,
                solution.ProposedAnswer.HasValue ? solution.ProposedAnswer.ToString() : "unknown",
                solution.ExpectedAnswer.HasValue ? solution.ExpectedAnswer.ToString() : "unknown",
                (int)solution.ElapsedTime.TotalMilliseconds,
                result.LongDescription,
                solution.Exception == null ? "" : solution.Exception.GetType().Name);

            _logList.Add(logMessage);
        }
    }
}