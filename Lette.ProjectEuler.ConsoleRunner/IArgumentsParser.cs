namespace Lette.ProjectEuler.ConsoleRunner
{
    public interface IArgumentsParser
    {
        Settings Parse(string[] arguments);
    }
}