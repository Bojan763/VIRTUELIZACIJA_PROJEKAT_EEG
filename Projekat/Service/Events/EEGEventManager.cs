using Service.EventArgs;
using System;

namespace Service.Events
{
    public class EEGEventManager
    {
        public event EventHandler<TransferEventArgs> OnTransferStarted;

        public event EventHandler<TransferEventArgs> OnSampleReceived;

        public event EventHandler<TransferEventArgs> OnTransferCompleted;

        public event EventHandler<WarningEventArgs> OnWarningRaised;

        public event EventHandler<AnalyticsEventArgs> OnAttentionSpike;

        public void RaiseTransferStarted(string participantId)
        {
            OnTransferStarted?.Invoke(this, new TransferEventArgs
                {
                    ParticipantId = participantId
                });
        }

        public void RaiseSampleReceived(int count)
        {
            OnSampleReceived?.Invoke(this, new TransferEventArgs
                {
                    SampleCount = count
                });
        }

        public void RaiseTransferCompleted(int count)
        {
            OnTransferCompleted?.Invoke(this, new TransferEventArgs
                {
                    SampleCount = count
                });
        }

        public void RaiseWarning(WarningType type, string message)
        {
            OnWarningRaised?.Invoke(this, new WarningEventArgs
            {
                WarningType = type,
                Message = message
            });
        }
        public void RaiseAttentionSpike(AnalyticsEventArgs args)
        {
            OnAttentionSpike?.Invoke(this, args);
        }
    }
}