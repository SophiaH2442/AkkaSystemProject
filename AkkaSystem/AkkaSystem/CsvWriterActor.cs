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
            string s = writeNumberMessage.ToString();
            string[] t = null;
            t = s.Split(' ');

            foreach (var item in t)
            {
                if (Int32.Parse(item) % 2 == 0)
                {
                    string[] array = { item };
                    string separator = ",";
                    string joined = String.Join(separator, array);
                    Console.WriteLine(joined);

                    var evenfilePathAppConfig = ConfigurationManager.AppSettings["EvenFilePath"];

                    using (StreamWriter writer = new StreamWriter(evenfilePathAppConfig, append: true))
                    {
                        writer.WriteLine(joined);
                    }
                }
                else
                {
                    string[] oddArray = { item };
                    string oddSeparator = ",";
                    string oddJoined = String.Join(oddSeparator, oddArray);
                    Console.WriteLine(oddJoined);

                    var oddfilePathAppConfig = ConfigurationManager.AppSettings["OddFilePath"];

                    using (StreamWriter writer = new StreamWriter(oddfilePathAppConfig, append: true))
                    {
                        writer.WriteLine(oddJoined);
                    }
                }
            }

        }
    }
}