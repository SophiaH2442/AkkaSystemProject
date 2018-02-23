namespace AkkaSystem
{
    public class EvenOpenFile
    {
        public EvenOpenFile(string evenFilePath)
        {
            EvenFilePath = evenFilePath;
        }
        public string EvenFilePath { get; }
    }
}