using Client.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            EEGCsvLoader loader = new EEGCsvLoader();
            CsvParser parser = new CsvParser();

            var files = loader.FindFiles("EEG");

            foreach (var file in files)
            {
                Console.WriteLine($"Participant: {file.participantId}");
                var samples = parser.ParseFile(file.path);
                Console.WriteLine($"Loaded: {samples.Count}");
            }
            Console.ReadLine();

        }
    }
}
