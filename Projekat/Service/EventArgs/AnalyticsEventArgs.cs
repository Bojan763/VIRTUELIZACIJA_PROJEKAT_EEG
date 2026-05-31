using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EventArgs
{
    public class AnalyticsEventArgs : System.EventArgs
    {
        public string ParticipantId { get; set; }

        public DateTime Timestamp { get; set; }

        public int RowIndex { get; set; }

        public double PreviousValue { get; set; }

        public double CurrentValue { get; set; }

        public double Delta { get; set; }

        public string Metric { get; set; }

        public string Direction { get; set; }
    }
}
