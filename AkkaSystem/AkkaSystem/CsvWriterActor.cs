using System;
using System.Configuration;
using System.IO;
using Akka.Actor;

namespace AkkaSystem
{
    public class CsvWriterActor : ReceiveActor
    {
        public CsvWriterActor()
        {
            Receive<WriteNumber>(writeNumberMessage => WriteCsv(writeNumberMessage));
        }

        protected void WriteCsv(WriteNumber writeNumberMessage)
        {
            if (writeNumberMessage.Number % 2 == 0)
            {
                Console.WriteLine(writeNumberMessage);

                var evenfilePathAppConfig = ConfigurationManager.AppSettings["EvenFilePath"];

                using (StreamWriter writer = new StreamWriter(evenfilePathAppConfig, append: true))
                {
                    writer.WriteLine(writeNumberMessage);
                    writer.Close();
                }
            }
            else
            {
                Console.WriteLine(writeNumberMessage);

                var oddfilePathAppConfig = ConfigurationManager.AppSettings["OddFilePath"];

                using (StreamWriter writer = new StreamWriter(oddfilePathAppConfig, append: true))
                {
                    writer.WriteLine(writeNumberMessage);
                    writer.Close();
                }
            }
        }
    }
}
