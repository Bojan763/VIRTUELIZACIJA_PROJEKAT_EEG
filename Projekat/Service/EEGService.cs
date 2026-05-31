using Common;
using Service.EventArgs;
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

        private double lastAttention = -1;
        private double lastEngagement = -1;

        private int receivedSamples = 0;

        private string currentParticipantId; //da bismo u dogadjaj za engagement cuvali trenutnog pariticpantId

        public EEGService()
        {
            eventManager.OnTransferStarted += handlers.TransferStartedHandler;

            eventManager.OnSampleReceived += handlers.SampleReceivedHandler;

            eventManager.OnTransferCompleted += handlers.TransferCompletedHandler;

            eventManager.OnWarningRaised += handlers.WarningHandler;

            eventManager.OnAttentionSpike +=  handlers.AttentionSpikeHandler;

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
           int attentionSpikeThreshold = int.Parse(ConfigurationManager.AppSettings["AttentionSpikeThreshold"]);
           int engagementDropThreshold = int.Parse(ConfigurationManager.AppSettings["EngagementDropThreshold"]);
           validator.ValidateSample(sample);

            // -------- ATTENTION ----------
            if (lastAttention != -1)
            {
                double deltaAttention = sample.Attention - lastAttention;

                if (Math.Abs(deltaAttention) > attentionSpikeThreshold)
                {
                    eventManager.RaiseAttentionSpike(
                        new AnalyticsEventArgs
                        {
                            ParticipantId = currentParticipantId,
                            Timestamp = sample.TimeStamp,
                            RowIndex = sample.RowIndex,
                            PreviousValue = lastAttention,
                            CurrentValue = sample.Attention,
                            Delta = deltaAttention,
                            Metric = "Attention",
                            Direction = deltaAttention > 0 ? "UP" : "DOWN"
                        });
                }
            }

            lastAttention = sample.Attention;

            // -------- ENGAGEMENT ----------
            if (lastEngagement != -1)
            {
                double deltaEngagement = sample.Engagement - lastEngagement;

                if (deltaEngagement < -engagementDropThreshold)
                {
                    eventManager.RaiseWarning(
                        WarningType.EngagementDrop,
                        $"Engagement dropped: {deltaEngagement}");
                }
            }





            if (sample.Battery < batteryLowThreshold)
            {
                eventManager.RaiseWarning(WarningType.BatteryLow, $"Battery low: {sample.Battery}");
            }
            if (sample.ContactQuality < contactQualityMin)
            {
                eventManager.RaiseWarning(WarningType.ContactQualityLow, $"Contact quality low: {sample.ContactQuality}");
            }

            storage.SaveSample(sample);
            receivedSamples++;
            eventManager.RaiseSampleReceived(receivedSamples); //dogadjaj
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

            currentParticipantId = meta.ParticipantId;

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
