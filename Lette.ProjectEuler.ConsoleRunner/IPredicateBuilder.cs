using System;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public interface IPredicateBuilder
    {
        Func<int, bool> CreateFromFilter(string filter);
    }
}