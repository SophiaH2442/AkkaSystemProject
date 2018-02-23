using System.IO;

namespace AkkaSystem
{
    public interface IStreamWriterFactory 
    {
        StreamWriter Create(string filePath);
    }
}