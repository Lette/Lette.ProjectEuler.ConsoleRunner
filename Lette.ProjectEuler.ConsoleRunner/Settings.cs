namespace Lette.ProjectEuler.ConsoleRunner
{
    public class Settings
    {
        public Settings()
        {
            Parallel = true;
            Filter = string.Empty;
            FilePath = null;
        }

        public bool Parallel { get; set; }
        public string Filter { get; set; }
        public string FilePath { get; set; }
    }
}