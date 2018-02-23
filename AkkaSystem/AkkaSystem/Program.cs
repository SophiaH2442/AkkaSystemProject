using System.Configuration;
using System.IO;
using Akka.Actor;

namespace AkkaSystem
{
    public class Program
    {
        public static ActorSystem MyActorSystem;
        static void Main(string[] args)
        {
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            StreamWriterFactory writerEvenFactory = new StreamWriterFactory();
            Props csvWriterProps = Props.Create(() => new CsvWriterActor(writerEvenFactory));
            IActorRef csvWriterActor = MyActorSystem.ActorOf(csvWriterProps, "csvWriterActor");

            StreamReaderFactory readerFactory = new StreamReaderFactory();
            Props csvReaderProps = Props.Create(() => new CsvReaderActor(csvWriterActor, readerFactory));
            IActorRef csvReaderActor = MyActorSystem.ActorOf(csvReaderProps, "csvReaderActor");

            var readFileMessage = new ReadFile(ConfigurationManager.AppSettings["FilePath"]);
            csvReaderActor.Tell(readFileMessage);

            MyActorSystem.WhenTerminated.Wait();
        }
    }
}
