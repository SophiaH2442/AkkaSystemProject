using Akka.TestKit.NUnit;
using NUnit.Framework;

namespace AkkaSystemTests
{
    [TestFixture]
    public class Tests : TestKit
    {
        //    private CsvWriterActorFactory _createCsvWriterTestProbe;

        //    [Test]
        //    public void OnStartup_CreatesCsvWriterActor()
        //    {
        //        var csvWriterActorCreated = false;
        //        CsvWriterActorFactory checkCsvWriterActorCreated = (context, parameters, name) =>
        //        {
        //            Assert.That(context, Is.Not.Null);
        //            Assert.That(parameters, Is.Not.Null);
        //            Assert.That(name, Is.Not.Null);
        //            csvWriterActorCreated = true;
        //            return CreateTestProbe();
        //        };

        //        ActorOf(GetCsvWriterActorProps(csvWriterActorFactoryOverride: checkCsvWriterActorCreated));

        //        AwaitCondition(() => csvWriterActorCreated);
        //    }

        //    private Props GetCsvWriterActorProps(
        //        CsvWriterActorFactory csvWriterActorFactoryOverride = null)
        //    {
        //        var csvWriter = csvWriterActorFactoryOverride ?? _createCsvWriterTestProbe;

        //        return Props.Create(() => new CsvWriterActor());
        //    }
        //}

        //public delegate IActorRef CsvWriterActorFactory(IActorContext context, CsvWriterActor parameters,
        //    string name);

        [Test]
        public void OnStartup_CreateCsvWriterActor()
        {
            
        }

    }
}
