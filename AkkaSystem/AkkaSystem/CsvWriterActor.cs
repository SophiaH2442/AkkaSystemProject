using System;
using System.Configuration;
using System.IO;
using Akka.Actor;

namespace AkkaSystem
{
    public class WriteFile
    {
        public WriteFile(string evenFilePath)
        {
            EvenFilePath = evenFilePath;
        }

        public string EvenFilePath { get; set; }

    }
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
                    string[] array = new string[] { item.ToString() };
                    string separator = ",";
                    string joined = String.Join(separator, array);
                    Console.WriteLine(joined);

                    //var evenfilePathAppConfig = ConfigurationManager.AppSettings["EvenFilePath"];

                    //using (StreamWriter writer = new StreamWriter(evenfilePathAppConfig))
                    //{

                    //}
                }
            }

        }
    }
}
