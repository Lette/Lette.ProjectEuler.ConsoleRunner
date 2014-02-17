using System;
using Xunit;
using Xunit.Extensions;

namespace Lette.ProjectEuler.ConsoleRunner.Tests
{
    public class ArgumentsParserTests
    {
        private readonly ArgumentsParser _parser;
        private Settings _settings;

        public ArgumentsParserTests()
        {
            _parser = new ArgumentsParser();
        }

        [Fact]
        public void ASingleArgumentMustBeTheFilePath()
        {
            Parse("file.dll");

            Assert.Equal("file.dll", _settings.FilePath);
        }

        [Fact]
        public void UnknownArgumentThrows()
        {
            var exception = Assert.Throws<Exception>(() => Parse("--unknown-argument"));

            Assert.Equal("Unknown argument: --unknown-argument", exception.Message);
        }

        [Theory]
        [InlineData("-r")]
        [InlineData("--run")]
        [InlineData("--RUN")]
        public void FilePathIsReadWithRunArgument(string argument)
        {
            Parse(argument + " file.dll");

            Assert.Equal("file.dll", _settings.FilePath);
        }

        [Fact]
        public void ParallelExecutionIsOnByDefault()
        {
            Parse("file.dll");

            Assert.True(_settings.Parallel);
        }

        [Theory]
        [InlineData("-p+")]
        [InlineData("-P+")]
        [InlineData("--para")]
        [InlineData("--PARA")]
        [InlineData("--parallel")]
        [InlineData("--PARALLEL")]
        public void ParallelExecutionCanBeTurnedOn(string argument)
        {
            Parse("file.dll " + argument);

            Assert.True(_settings.Parallel);
        }

        [Theory]
        [InlineData("-p-")]
        [InlineData("-P-")]
        [InlineData("--seq")]
        [InlineData("--SEQ")]
        [InlineData("--sequential")]
        [InlineData("--SEQUENTIAL")]
        public void ParallelExecutionCanBeTurnedOff(string argument)
        {
            Parse("file.dll " + argument);

            Assert.False(_settings.Parallel);
        }

        [Fact]
        public void FilterIsEmptyStringAsDefault()
        {
            Parse("file.dll");

            Assert.Equal("", _settings.Filter);
        }

        [Theory]
        [InlineData("-f")]
        [InlineData("-F")]
        [InlineData("--filter")]
        [InlineData("--FILTER")]
        public void CanParseFilterArgument(string argument)
        {
            Parse("file.dll " + argument + " filter-expression");

            Assert.Equal("filter-expression", _settings.Filter);
        }

        [Fact]
        public void CanParseAllArgumentsTogether()
        {
            Parse("-f filter-expression --SEQ file-path");

            Assert.Equal("filter-expression", _settings.Filter);
            Assert.False(_settings.Parallel);
            Assert.Equal("file-path", _settings.FilePath);
        }

        [Fact]
        public void MissingSecondArgumentThrows()
        {
            var exception = Assert.Throws<Exception>(() => Parse("-f"));

            Assert.Equal("Arguments missing.", exception.Message);
        }

        private void Parse(string args)
        {
            _settings = _parser.Parse(("program.exe " + args).Split(' '));
        }
    }
}