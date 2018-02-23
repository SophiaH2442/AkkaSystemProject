using System.IO;

namespace AkkaSystem
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriter Create(string evenFilePath)
        {
            return new StreamWriter(evenFilePath);
        }
    }
}