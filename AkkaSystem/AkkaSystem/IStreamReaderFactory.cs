using System.IO;

namespace AkkaSystem
{
    public interface IStreamReaderFactory
    {
        StreamReader Create(string filePath);
    }
}