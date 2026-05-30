using Common;
using System;

namespace Service.Handlers
{
    public class EventHandlers
    {
        public void TransferStartedHandler(object sender, TransferEventArgs e)
        {
            Console.WriteLine($"Transfer started for participant {e.ParticipantId}");
        }

        public void SampleReceivedHandler(object sender, TransferEventArgs e)
        {
            Console.WriteLine($"Received samples: {e.SampleCount}");
        }

        public void TransferCompletedHandler(object sender, TransferEventArgs e)
        {
            Console.WriteLine($"Transfer completed. Total samples: {e.SampleCount}");
        }

        public void WarningHandler(object sender, WarningEventArgs e)
        {
            Console.WriteLine($"WARNING [{e.WarningType}] {e.Message}");
        }
    }
}