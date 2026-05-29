using Common;

using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EEGService : IEEGService
    {


        private EEGValidator validator = new EEGValidator(); //za validacije
        private EEGStorage storage = new EEGStorage(); // cuvanje na disk
        private int receivedSamples = 0;

        public EEGService()
        {
            Console.WriteLine("EEGService ctor: " + GetHashCode());
        }
        public AckResponse EndSession()
        {
            Console.WriteLine("EndSession: " + GetHashCode());
            storage.Dispose();

            Console.WriteLine("Završen prenos.");

            Console.WriteLine($"Ukupno primljeno uzoraka: " + $"{receivedSamples}");

            return new AckResponse
            {
                Success = true,
                Message = "Session ended",
                Status = "COMPLETED"

            };
        }
        public AckResponse PushSample(EegSample sample)
        {
            Console.WriteLine($"Prenos u toku... " + $"RowIndex={sample.RowIndex}");
            validator.ValidateSample(sample);

           
            storage.SaveSample(sample);
           
            receivedSamples++;
            return new AckResponse
            {
                Success = true,
                Message = "Sample received",
                Status = "IN_PROGRESS"
            };
        }

        public AckResponse StartSession(EegMeta meta)
        {
            Console.WriteLine("StartSession: " + GetHashCode());
            receivedSamples = 0;
            storage.Start(meta);

            Console.WriteLine($"Session started for participant " + $"{meta.ParticipantId}");
            return new AckResponse
            {
                Success = true,
                Message = "Session started",
                Status = "IN_PROGRESS"

            };
        }
    }
}
