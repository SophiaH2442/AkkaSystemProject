using System;
using System.Configuration;
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


    public class CsvWriterActor : ReceiveActor
    {
        private StreamWriter _evenWriter;
        private StreamWriter _oddWriter;

        public CsvWriterActor()
        {
            Receive<WriteNumber>(writeNumberMessage => WriteCsv(writeNumberMessage));
            Receive<EvenOpenFile>(message => StartWritingEven(message));
            Receive<OddOpenFile>(message => StartWritingOdd(message));
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
                _evenWriter.WriteLine(writeNumberMessage);
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
                _oddWriter.WriteLine(writeNumberMessage);
                _oddWriter.Flush();
            }
        }
    }
}
