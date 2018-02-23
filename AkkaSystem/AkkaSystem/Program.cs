using Akka.Actor;
using System.Configuration;

namespace AkkaSystem
{
    public static class Program
    {
        private static ActorSystem _myActorSystem;

        private static void Main()
        {
            _myActorSystem = ActorSystem.Create("MyActorSystem");

            var writerEvenFactory = new StreamWriterFactory();
            var csvWriterProps = Props.Create(() => new CsvWriterActor(writerEvenFactory));
            var csvWriterActor = _myActorSystem.ActorOf(csvWriterProps, "csvWriterActor");

            var readerFactory = new StreamReaderFactory();
            var csvReaderProps = Props.Create(() => new CsvReaderActor(csvWriterActor, readerFactory));
            var csvReaderActor = _myActorSystem.ActorOf(csvReaderProps, "csvReaderActor");

            var readFileMessage = new ReadFile(ConfigurationManager.AppSettings["FilePath"]);
            csvReaderActor.Tell(readFileMessage);

            _myActorSystem.WhenTerminated.Wait();
        }
    }
}
