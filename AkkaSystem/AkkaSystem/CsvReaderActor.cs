using Akka.Actor;
using System.Configuration;

namespace AkkaSystem
{
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

        private void ReadCsv(ReadFile message)
        {
            // Open files
            var evenFilePathAppConfig = new EvenOpenFile(ConfigurationManager.AppSettings["EvenFilePath"]);
            _csvWriterActor.Tell(evenFilePathAppConfig);

            var oddFilePathAppConfig = new OddOpenFile(ConfigurationManager.AppSettings["OddFilePath"]);
            _csvWriterActor.Tell(oddFilePathAppConfig);
            
            // Write to them
            using (var reader = _streamReaderFactory.Create(message.FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var strline = reader.ReadLine();
                    if (strline == null) continue;

                    var values = strline.Split(',');

                    foreach (var item in values)
                    {
                        var writeNumberMessage = new WriteNumber(int.Parse(item));
                        _csvWriterActor.Tell(writeNumberMessage);
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