using System;
using System.Linq;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class ArgumentsParser : IArgumentsParser
    {
        private Settings _settings;

        private readonly ArgumentsRulesList _rules = new ArgumentsRulesList
            {
                { new[] { "-p+", "--para", "--parallel" }, (s, p) => s.Parallel = true, 0 },
                { new[] { "-p-", "--seq", "--sequential" }, (s, p) => s.Parallel = false, 0 },
                { new[] { "-f", "--filter" }, (s, p) => s.Filter = p, 1 },
                { new[] { "-r", "--run" }, (s, p) => s.FilePath = p, 1 },
            };

        public Settings Parse(string[] arguments)
        {
            _settings = new Settings();

            Parse(arguments, 1);

            return _settings;
        }

        private void Parse(string[] arguments, int index)
        {
            if (index >= arguments.Length)
            {
                return;
            }

            var rule = _rules
                .FirstOrDefault(
                    x => x.Arguments.Contains(arguments[index].ToLowerInvariant()));

            if (rule == null)
            {
                if (arguments[index].StartsWith("-"))
                {
                    throw new Exception("Unknown argument: " + arguments[index]);
                }

                _settings.FilePath = arguments[index];
            }
            else
            {
                index += rule.Delta;

                if (index >= arguments.Length)
                {
                    throw new Exception("Arguments missing.");
                }

                rule.Setter(_settings, arguments[index]);
            }

            Parse(arguments, ++index);
        }
    }
}