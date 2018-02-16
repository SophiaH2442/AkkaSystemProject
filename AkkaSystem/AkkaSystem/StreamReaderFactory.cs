using System.IO;

namespace AkkaSystem
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
        public StreamReader Create(string filePath)
        {
            return new StreamReader(filePath);
        }
    }
}