using System;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class ArgumentsRule
    {
        public ArgumentsRule(string[] arguments, Action<Settings, string> setter, int delta)
        {
            Arguments = arguments;
            Setter = setter;
            Delta = delta;
        }

        public string[] Arguments { get; private set; }
        public Action<Settings, string> Setter { get; private set; }
        public int Delta { get; private set; }
    }
}