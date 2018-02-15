using System;
using System.IO;
using Akka.Actor;

namespace AkkaSystem
{
    public class EvenOpenFile
    {
        public EvenOpenFile(string evenFilePath)
        {
            EvenFilePath = evenFilePath;
        }
        public string EvenFilePath { get; set; }
    }

    public class OddOpenFile
    {
        public OddOpenFile(string oddFilePath)
        {
            OddFilePath = oddFilePath;
        }

        public string OddFilePath { get; set; }
    }

    public class OddCloseFile
    {
    }

    public class EvenCloseFile
    {
    }

    public class CsvWriterActor : ReceiveActor
    {
        private StreamWriter _evenWriter;
        private StreamWriter _oddWriter;

        public CsvWriterActor()
        {
            Receive<WriteNumber>(writeNumberMessage => WriteCsv(writeNumberMessage));
            Receive<EvenOpenFile>(message => StartWritingEven(message));
            Receive<OddOpenFile>(message => StartWritingOdd(message));
            Receive<EvenCloseFile>(message => StopWritingEven(message));
            Receive<OddCloseFile>(message => StopWritingOdd(message));
        }

        protected void StartWritingEven(EvenOpenFile message)
        {
            _evenWriter = new StreamWriter(message.EvenFilePath);
        }

        protected void StartWritingOdd(OddOpenFile message)
        {
            _oddWriter = new StreamWriter(message.OddFilePath);
        }

        protected void WriteCsv(WriteNumber writeNumberMessage)
        {
            if (writeNumberMessage.Number % 2 == 0)
            {
                if (_evenWriter == null)
                {
                    Console.WriteLine("Cannot write even number");
                    return;
                }
                Console.WriteLine(writeNumberMessage);
                _evenWriter.Write(writeNumberMessage + ",");
                _evenWriter.Flush();
            }
            else
            {
                if (_oddWriter == null)
                {
                    Console.WriteLine("Cannot write odd number");
                    return;
                }
                Console.WriteLine(writeNumberMessage);
                _oddWriter.Write(writeNumberMessage + ",");
                _oddWriter.Flush();
            }
        }

        protected void StopWritingEven(EvenCloseFile message)
        {
            _evenWriter.Close();
            _evenWriter.Dispose();
            _evenWriter = null;
        }

        protected void StopWritingOdd(OddCloseFile message)
        {
            _oddWriter.Close();
            _oddWriter.Dispose();
            _oddWriter = null;
        }
    }
}
