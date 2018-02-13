using System;
using System.IO;
using Akka.Actor;

namespace AkkaSystem
{
    public class ReadFile
    {
        public ReadFile(string filePath)
        {
            FilePath = filePath;
        }
        public string FilePath { get; set; }
    }

    public class WriteNumber
    {
        public WriteNumber(int number)
        {
            Number = number;
        }

        public int Number { get; set; }
        public override string ToString()
        {
            return Number.ToString();
        }
    }

    public class CsvReaderActor : ReceiveActor
    {
        private IActorRef _csvWriterActor;

        public CsvReaderActor(IActorRef csvWriterActor)
        {
            Receive<ReadFile>(message => ReadCsv(message));
            _csvWriterActor = csvWriterActor;
        }


        protected void ReadCsv(ReadFile message)
        {
            using (StreamReader reader = new StreamReader(message.FilePath))
            {
                string strline = "";
                string[] values = null;
                while (!reader.EndOfStream)
                {
                    strline = reader.ReadLine();
                    values = strline.Split(',');

                    foreach (var item in values)
                    {
                        var writeNumberMessage = new WriteNumber(Int32.Parse(item));
                        _csvWriterActor.Tell(writeNumberMessage);
                    }
                }
            }
        }
    }
}