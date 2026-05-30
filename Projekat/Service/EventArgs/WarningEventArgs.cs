using System;

namespace Common
{
    public class WarningEventArgs : EventArgs
    {
        public WarningType WarningType { get; set; }
        public string Message { get; set; }
    }
}