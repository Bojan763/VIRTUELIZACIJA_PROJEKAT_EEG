using System;

namespace Common
{
    public class TransferEventArgs : EventArgs
    {
        public string ParticipantId { get; set; }
        public int SampleCount { get; set; }
    }
}