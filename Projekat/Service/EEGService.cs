using Common;
using Service.Events;
using Service.Handlers;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private EEGEventManager eventManager = new EEGEventManager(); // dogadjaji
        private EventHandlers handlers = new EventHandlers(); // handleri za dogadjaje

        private int receivedSamples = 0;

        public EEGService()
        {
            eventManager.OnTransferStarted += handlers.TransferStartedHandler;

            eventManager.OnSampleReceived += handlers.SampleReceivedHandler;

            eventManager.OnTransferCompleted += handlers.TransferCompletedHandler;

            eventManager.OnWarningRaised += handlers.WarningHandler;

        }
        public AckResponse EndSession()
        {
            eventManager.RaiseTransferCompleted(receivedSamples);
            storage.Dispose();

            return new AckResponse
            {
                Success = true,
                Message = "Session ended",
                Status = "COMPLETED"

            };
        }
        public AckResponse PushSample(EegSample sample)
        {
           int batteryLowThreshold = int.Parse(ConfigurationManager.AppSettings["BatteryLowThreshold"]);

           int contactQualityMin = int.Parse(ConfigurationManager.AppSettings["ContactQualityMin"]);

           validator.ValidateSample(sample);

            if (sample.Battery < batteryLowThreshold)
            {
                eventManager.RaiseWarning(WarningType.BatteryLow, $"Battery low: {sample.Battery}");
            }
            if (sample.ContactQuality < contactQualityMin)
            {
                eventManager.RaiseWarning(WarningType.ContactQualityLow, $"Contact quality low: {sample.ContactQuality}");
            }

            storage.SaveSample(sample);

            eventManager.RaiseSampleReceived(receivedSamples); //dogadjaj


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
            receivedSamples = 0;
            storage.Start(meta);

            eventManager.RaiseTransferStarted(meta.ParticipantId); //dogadjaj

            return new AckResponse
            {
                Success = true,
                Message = "Session started",
                Status = "IN_PROGRESS"

            };
        }
    }
}
