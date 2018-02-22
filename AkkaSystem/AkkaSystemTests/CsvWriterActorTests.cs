using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.TestKit;
using Akka.TestKit.NUnit;
using AkkaSystem;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AkkaSystemTests
{
    [TestFixture]
    public class CsvWriterActorTests : TestKit
    {
        //private MemoryStream _memoryStream;
        //private StreamWriter _evenStreamWriter;
        //private StreamWriter _oddStreamWriter;

        //[SetUp]
        //public void Setup()
        //{
        //    CreateEvenStreamWriter();
        //    CreateOddStreamWriter();
        //}

        //[Test]
        //public void WhenReceivesEvenOpenFileMessage_CreatesNewEvenStreamWriter()
        //{
        //    var actorInTest = CreateCsvWriterActor();
        //    var evenFilePath = "Dummy Path";
        //    actorInTest.Tell(new EvenOpenFile(evenFilePath));
            
        //}



        //private IActorRef CreateCsvWriterActor()
        //{
        //    return Sys.ActorOf(Props.Create(() => new CsvWriterActor()));
        //}

        //private void CreateEvenStreamWriter()
        //{
        //    _memoryStream = new MemoryStream();
        //    _evenStreamWriter = new StreamWriter(_memoryStream);
        //    _evenStreamWriter.Write(EvenFakeFileContent());
        //    _evenStreamWriter.Flush();
        //}

        //private string EvenFakeFileContent()
        //{
        //    return "2,4,30\n";
        //}

        //private void CreateOddStreamWriter()
        //{
        //    _memoryStream = new MemoryStream();
        //    _oddStreamWriter = new StreamWriter(_memoryStream);
        //    _oddStreamWriter.Write(OddFakeFileContent());
        //    _oddStreamWriter.Flush();
        //}

        //private string OddFakeFileContent()
        //{
        //    return "1,5,99\n";
        //}
    }
}
