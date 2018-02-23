using System;
using System.IO;
using Akka.Actor;

namespace AkkaSystem
{
    public class CsvWriterActor : ReceiveActor
    {
        private readonly IStreamWriterFactory _streamWriterFactory;
        private StreamWriter _oddWriter;
        private StreamWriter _evenWriter;

        public CsvWriterActor(IStreamWriterFactory streamWriterFactory)
        {
            _streamWriterFactory = streamWriterFactory;

            Receive<WriteNumber>(writeNumberMessage => WriteCsv(writeNumberMessage));
            Receive<EvenOpenFile>(message => StartWritingEven(message));
            Receive<OddOpenFile>(message => StartWritingOdd(message));
            Receive<EvenCloseFile>(message => StopWritingEven());
            Receive<OddCloseFile>(message => StopWritingOdd());
        }

        private void StartWritingEven(EvenOpenFile message)
        {
            _evenWriter = _streamWriterFactory.Create(message.EvenFilePath);
        }

        private void StartWritingOdd(OddOpenFile message)
        {
            _oddWriter = _streamWriterFactory.Create(message.OddFilePath);
        }

        private void WriteCsv(WriteNumber writeNumberMessage)
        {
            if (writeNumberMessage.Number % 2 == 0)
            {
                if (_evenWriter == null)
                {
                    Console.WriteLine("Cannot write even number");
                    return;
                }
                Console.WriteLine(writeNumberMessage);
                _evenWriter.Write(writeNumberMessage + ",");
                _evenWriter.Flush();
            }
            else
            {
                if (_oddWriter == null)
                {
                    Console.WriteLine("Cannot write odd number");
                    return;
                }
                Console.WriteLine(writeNumberMessage);
                _oddWriter.Write(writeNumberMessage + ",");
                _oddWriter.Flush();
            }
        }

        private void StopWritingEven()
        {
            _evenWriter.Close();
            _evenWriter.Dispose();
            _evenWriter = null;
        }

        private void StopWritingOdd()
        {
            _oddWriter.Close();
            _oddWriter.Dispose();
            _oddWriter = null;
        }
    }
}
