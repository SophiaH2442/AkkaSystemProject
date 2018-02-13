using System.Configuration;
using Akka.Actor;
using Akka.TestKit.NUnit;
using AkkaSystem;
using NUnit.Framework;

namespace AkkaSystemTests
{
    [TestFixture]
    public class Tests : TestKit
    {
        [Test]
        public void CsvReaderActor_should_readfile()
        {
            //IActorRef consoleWriterActor = null;
            //var actor = Sys.ActorOf(Props.Create(() => new CsvReaderActor(consoleWriterActor)));
            //actor.Tell(new ReadFile(ConfigurationManager.AppSettings["FilePath"]));
            //var result = ExpectMsg<ReadFile>
        }
    }
}
