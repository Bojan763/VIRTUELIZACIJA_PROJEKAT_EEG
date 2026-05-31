using System;

namespace Service.EventArgs

{
    public class TransferEventArgs : System.EventArgs
    {
        public string ParticipantId { get; set; }
        public int SampleCount { get; set; }
    }
}