using System;
using Lette.ProjectEuler.Core.Runner;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler;

            var writer = new Writer(Console.Out);
            var argumentsParser = new ArgumentsParser();
            var environment = new EnvironmentWrapper();
            var predicateBuilder = new PredicateBuilder();
            var solver = new Solver();
            var suiteBuilder = new ProblemSuiteBuilder();

            var controller = new RunnerController(writer, argumentsParser, environment, predicateBuilder, solver, suiteBuilder);

            controller.Start();
        }

        public static void ExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            var ex = eventArgs.ExceptionObject as Exception;

            var message = ex == null
                ? string.Format("Unknown error type was thrown. Type was: {0}", eventArgs.ExceptionObject.GetType().FullName)
                : "An unexpected error occurred. Technical details that might help you figure out what happened will follow.";

            Console.Out.WriteLine(message);
            LogError(ex);

            Environment.Exit(1);
        }

        private static void LogError(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            Console.Out.WriteLine();
            Console.Out.WriteLine("Type: {0}", exception.GetType().Name);
            Console.Out.WriteLine("Message: {0}", exception.Message);
            Console.Out.WriteLine("Stacktrace: {0}", exception.StackTrace);

            LogError(exception.InnerException);
        }
    }
}