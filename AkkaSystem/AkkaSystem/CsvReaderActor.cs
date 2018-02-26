using System;
using System.Configuration;
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
        private readonly IActorRef _csvWriterActor;
        private readonly IStreamReaderFactory _streamReaderFactory;

        public CsvReaderActor(IActorRef csvWriterActor, IStreamReaderFactory streamReaderFactory)
        {
            Receive<ReadFile>(message => ReadCsv(message));
            _csvWriterActor = csvWriterActor;
            _streamReaderFactory = streamReaderFactory;
        }

        protected void ReadCsv(ReadFile message)
        {
            // Open files
            var evenFilePathAppConfig = new EvenOpenFile(ConfigurationManager.AppSettings["EvenFilePath"]);
            _csvWriterActor.Tell(evenFilePathAppConfig);

            var oddFilePathAppConfig = new OddOpenFile(ConfigurationManager.AppSettings["OddFilePath"]);
            _csvWriterActor.Tell(oddFilePathAppConfig);
            
            // Write to them
            using (StreamReader reader = _streamReaderFactory.Create(message.FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var strline = reader.ReadLine();
                    if (strline != null)
                    {
                        var values = strline.Split(',');

                        foreach (var item in values)
                        {
                            var writeNumberMessage = new WriteNumber(Int32.Parse(item));
                            _csvWriterActor.Tell(writeNumberMessage);
                        }
                    }
                }
            }

            // Close the files
            var evenCloseFileMessage = new EvenCloseFile();
            _csvWriterActor.Tell(evenCloseFileMessage);

            var oddCloseFileMessage = new OddCloseFile();
            _csvWriterActor.Tell(oddCloseFileMessage);
        }
    }
}