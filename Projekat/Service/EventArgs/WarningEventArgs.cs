using System;
using Common;
namespace Service.EventArgs
{
    public class WarningEventArgs : System.EventArgs
    {
        public WarningType WarningType { get; set; }
        public string Message { get; set; }
    }
}