using System;
using System.Collections.Generic;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class ArgumentsRulesList : List<ArgumentsRule>
    {
        public void Add(string[] arguments, Action<Settings, string> setter, int delta)
        {
            Add(new ArgumentsRule(arguments, setter, delta));
        }
    }
}