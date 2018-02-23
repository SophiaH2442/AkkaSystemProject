namespace AkkaSystem
{
    public class ReadFile
    {
        public ReadFile(string filePath)
        {
            FilePath = filePath;
        }
        public string FilePath { get; }
    }
}