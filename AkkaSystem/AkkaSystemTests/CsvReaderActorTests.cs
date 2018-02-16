using System;
using System.IO;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.NUnit;
using AkkaSystem;
using Moq;
using NUnit.Framework;

namespace AkkaSystemTests
{
    [TestFixture]
    public class CsvReaderActorTests : TestKit
    {
        private TestProbe _csvWriterActor;
        private MemoryStream _memoryStream;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        private Mock<IStreamReaderFactory> _streamReaderFactory;

        [SetUp]
        public void Setup()
        {
            _csvWriterActor = CreateTestProbe();
            CreateStreamReader();
            _streamReaderFactory = new Mock<IStreamReaderFactory>();
        }

        [Test]
        public void WhenReceivesReadFileMessage_TellsCsvWriterActorToOpenEvenFile()
        {
            var actorInTest = CreateCsvReaderActor();
            var filePath = "Dummy Path";
            _streamReaderFactory.Setup(f => f.Create(filePath)).Returns(_streamReader);
            actorInTest.Tell(new ReadFile(filePath));
            AwaitAssert(() => _csvWriterActor.ExpectMsg<EvenOpenFile>(), TimeSpan.FromSeconds(5));
        }

        [Test]
        public void WhenReceivesReadFileMessage_TellsCsvWriterActorToOpenOddFile()
        {
            var actorInTest = CreateCsvReaderActor();
            var filePath = "Dummy Path";
            _streamReaderFactory.Setup(f => f.Create(filePath)).Returns(_streamReader);
            actorInTest.Tell(new ReadFile(filePath));
            AwaitAssert(() => _csvWriterActor.ExpectMsg<OddOpenFile>(), TimeSpan.FromSeconds(5));
        }

        private IActorRef CreateCsvReaderActor()
        {
            return Sys.ActorOf(Props.Create(() => new CsvReaderActor(_csvWriterActor, _streamReaderFactory.Object)));
        }

        private void CreateStreamReader()
        {
            _memoryStream = new MemoryStream();
            _streamWriter = new StreamWriter(_memoryStream);
            _streamWriter.Write(FakeFileContent());
            _streamWriter.Flush();
            _memoryStream.Position = 0;
            _streamReader = new StreamReader(_memoryStream);
        }

        private string FakeFileContent()
        {
            return "1,2,3,4\n";
        }
    }
}
