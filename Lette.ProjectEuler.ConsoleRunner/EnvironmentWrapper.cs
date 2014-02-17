using System;

namespace Lette.ProjectEuler.ConsoleRunner
{
    public class EnvironmentWrapper : IEnvironmentWrapper
    {
        public string[] GetCommandLineArgs()
        {
            return Environment.GetCommandLineArgs();
        }
    }
}