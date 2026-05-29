using Client.CSV;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
            string eegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EEG");
            var files = loader.FindFiles(eegPath);

            //KONEKCIJA PREMA SERVERU
            ChannelFactory<IEEGService> factory = new ChannelFactory<IEEGService>("EEGEndpoint");
            IEEGService proxy = factory.CreateChannel();
         
            try
            {

                foreach (var file in files) 
                {
                    Console.WriteLine($"Participant: " + $"{file.participantId}");
                    var samples = parser.ParseFile(file.path);
                    // META
                    EegMeta meta = new EegMeta
                        {
                            ParticipantId = file.participantId,

                            FileName =Path.GetFileName(file.path),

                            TotalRows = samples.Count,

                            SchemaVersion = "1.0"
                        };
                    // START SESSION
                    AckResponse startResp = proxy.StartSession(meta);
                    Console.WriteLine(startResp.Message);
                    // STREAMING
                    foreach (var sample in samples)
                    {
                        Console.WriteLine("KLIJENT: " + sample.RowIndex);
                        AckResponse resp = proxy.PushSample(sample);
                        Console.WriteLine(resp.Status);
                    }
                    // END SESSION
                    AckResponse endResp = proxy.EndSession();
                    Console.WriteLine(endResp.Message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

        }
    }
}
