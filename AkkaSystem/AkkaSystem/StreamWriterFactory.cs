using System.IO;

namespace AkkaSystem
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriter Create(string filePath)
        {
            return new StreamWriter(filePath);
        }
    }
}