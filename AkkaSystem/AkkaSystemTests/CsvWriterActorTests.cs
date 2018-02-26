using System;
using System.IO;
using Akka.Actor;
using Akka.TestKit.NUnit;
using AkkaSystem;
using Moq;
using NUnit.Framework;

namespace AkkaSystemTests
{
    [TestFixture]
    public class CsvWriterActorTests : TestKit
    {
        private MemoryStream _memoryStream;
        private StreamWriter _streamWriter;
        private Mock<IStreamWriterFactory> _streamWriterFactory;

        [SetUp]
        public void Setup()
        {
            CreateStreamWriter();
            _streamWriterFactory = new Mock<IStreamWriterFactory>();
        }

        [Test]
        public void WhenReceivesEvenOpenFileMessage_CreatesNewStreamWriter()
        {
            // Arrange
            //passed into Actor via EvenOpenFile and Actor should pass to the Streamwriter when calling Create()
            var evenFilePath = "Pickles and jams.txt"; 

            // If writerFactory.Create() is called with this exact string THEN return writer
            _streamWriterFactory.Setup(f => f.Create(evenFilePath)).Returns(_streamWriter);
            var actorInTest = CreateCsvWriterActor();
            // Act
            actorInTest.Tell(new EvenOpenFile(evenFilePath));

            // Assert
            AwaitAssert(() => 
                    _streamWriterFactory.VerifyAll() // Proves actor calls streamWriterFactory with correct filename
            , TimeSpan.FromSeconds(50)); // Actor is async, so if pause is not here, method may not have run yet
        }

        [Test]
        public void WhenReceivesOddOpenFileMessage_CreatesNewStreamWriter()
        {
            var oddFilePath = "Sam Vimes";
            _streamWriterFactory.Setup(f => f.Create(oddFilePath)).Returns(_streamWriter);
            var actorInTest = CreateCsvWriterActor();

            actorInTest.Tell(new OddOpenFile(oddFilePath));

            AwaitAssert(() => _streamWriterFactory.VerifyAll(), TimeSpan.FromSeconds(5));
            AwaitAssert(() => _streamWriterFactory.Verify(f => f.Create("Sam Vimes")), TimeSpan.FromSeconds(5));
        }

        [Test]
        public void WhenReceivesCloseEvenFileMessage_ClosesStreamWriter()
        {
            var evenFilePath = "Pickles and jams.txt";

            _streamWriterFactory.Setup(f => f.Create(evenFilePath)).Returns(_streamWriter);
            var actorInTest = CreateCsvWriterActor();

            actorInTest.Tell(new EvenOpenFile(evenFilePath));
            actorInTest.Tell(new EvenCloseFile());

            AwaitAssert(() => _streamWriterFactory.VerifyAll(), TimeSpan.FromSeconds(5));
        }

        [Test]
        public void WhenReceivesCloseOddFileMessage_ClosesStreamWriter()
        {
            var oddFilePath = "Sam Vimes";
            _streamWriterFactory.Setup(f => f.Create(oddFilePath)).Returns(_streamWriter);
            var actorInTest = CreateCsvWriterActor();

            actorInTest.Tell(new OddOpenFile(oddFilePath));
            actorInTest.Tell(new OddCloseFile());

            AwaitAssert(() => _streamWriterFactory.VerifyAll(), TimeSpan.FromSeconds(5));
        }

        private IActorRef CreateCsvWriterActor()
        {
            return Sys.ActorOf(Props.Create(() => new CsvWriterActor(_streamWriterFactory.Object)));
        }

        private void CreateStreamWriter()
        {
            _memoryStream = new MemoryStream();
            _streamWriter = new StreamWriter(_memoryStream);
            _streamWriter.Write(FakeFileContent());
            _streamWriter.Flush();
        }

        private string FakeFileContent()
        {
            return "2,4,7,30,99\n";
        }
    }
}
