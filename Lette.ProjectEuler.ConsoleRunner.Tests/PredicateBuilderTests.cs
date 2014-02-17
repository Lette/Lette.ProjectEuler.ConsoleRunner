using System;
using Xunit;

namespace Lette.ProjectEuler.ConsoleRunner.Tests
{
    public class PredicateBuilderTests
    {
        private readonly IPredicateBuilder _builder = new PredicateBuilder();
        private Func<int, bool> _predicate;

        [Fact]
        public void DefaultPredicateReturnsTrue()
        {
            Create(null);

            AssertPredicateIsTrue(0);
        }

        [Fact]
        public void PredicateReturnsTrueForGivenSingleNumber()
        {
            Create("1");

            AssertPredicateIsTrue(1);
        }

        [Fact]
        public void PredicateReturnsFalseForOtherNumberThanTheGivenSingleNumber()
        {
            Create("1");

            AssertPredicateIsFalse(2);
        }

        [Fact]
        public void FilterCanContainMultipleCommaSeparatedNumbers()
        {
            Create("1,3");

            AssertPredicateIsTrue(1, 3);
            AssertPredicateIsFalse(0, 2, 4);
        }

        [Fact]
        public void FilterCanContainIntervals()
        {
            Create("1-3");

            AssertPredicateIsFalse(0, 4);
            AssertPredicateIsTrue(1, 2, 3);
        }

        [Fact]
        public void FilterCanContainIntervalsWithoutLowerBound()
        {
            Create("-2");

            AssertPredicateIsTrue(1, 2);
            AssertPredicateIsFalse(3);
        }

        [Fact]
        public void FilterCanContainIntervalsWithoutUpperBound()
        {
            Create("5-");

            AssertPredicateIsFalse(4);
            AssertPredicateIsTrue(5, 6);
        }

        [Fact]
        public void FiltersCanBeCombined()
        {
            Create("-5,10,12,14-16,18,20-");

            AssertPredicateIsTrue(1, 4, 5, 10, 12, 14, 15, 16, 18, 20, 21, 999);
            AssertPredicateIsFalse(6, 9, 11, 13, 17, 19);
        }

        private void Create(string intervals)
        {
            _predicate = _builder.CreateFromFilter(intervals);
        }

        private void AssertPredicateIsTrue(params int[] numbers)
        {
            foreach (var number in numbers)
            {
                Assert.True(_predicate(number));
            }
        }

        private void AssertPredicateIsFalse(params int[] numbers)
        {
            foreach (var number in numbers)
            {
                Assert.False(_predicate(number));
            }
        }
    }
}