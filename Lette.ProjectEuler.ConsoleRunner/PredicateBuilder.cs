using System;
using System.Collections.Generic;
using System.Linq;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class PredicateBuilder : IPredicateBuilder
    {
        public Func<int, bool> CreateFromFilter(string filter)
        {
            if (filter == null)
            {
                return i => true;
            }

            var predicates = new List<Func<int, bool>>();

            var items = filter.Split(',');

            foreach (var item in items)
            {
                var localItem = item; // avoids problem with lambda closure

                if (item.StartsWith("-"))
                {
                    predicates.Add(i => i <= int.Parse(localItem.Substring(1)));
                }
                else if (item.EndsWith("-"))
                {
                    predicates.Add(i => int.Parse(localItem.Substring(0, localItem.Length - 1)) <= i);
                }
                else if (item.Contains("-"))
                {
                    var bounds = item.Split('-').Select(int.Parse).ToList();
                    predicates.Add(i => bounds[0] <= i && i <= bounds[1]);
                }
                else
                {
                    predicates.Add(i => i == int.Parse(localItem));
                }
            }

            return i => predicates.Any(x => x(i));
        }
    }
}