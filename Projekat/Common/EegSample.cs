using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class EegSample
    {
        [DataMember]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        public double AF3 { get; set; }
        [DataMember]
        public double T7 { get; set; }

        [DataMember]
        public double Pz { get; set; }
        [DataMember]
        public double T8 { get; set; }
        [DataMember]
        public double AF4 { get; set; }
        [DataMember]
        public double Attention { get; set; }
        [DataMember]
        public double Engagement { get; set; }
        [DataMember]
        public double Excitement { get; set; }
        [DataMember]
        public double Interest  { get; set; }
        [DataMember]
        public double Relaxation { get; set; }
        [DataMember]
        public double Stress { get; set; }
        [DataMember]
        public int Battery { get; set; }
        [DataMember]
        public int ContactQuality { get; set; }
        [DataMember]
        public int SlideIndex { get; set; }
        [DataMember]
        public int SetIndex { get; set; }
        [DataMember]
        public int RowIndex { get; set; }

        public EegSample(DateTime timeStamp, double aF3, double t7, double pz, double t8, double aF4, double attention, double engagement, double excitement, double interest, double relaxation, double stress, int battery, int contactQuality, int slideIndex, int setIndex, int rowIndex)
        {
            this.TimeStamp = timeStamp;
            this.AF3 = aF3;
            this.T7 = t7;
            this.Pz = pz;
            this.T8 = t8;
            this.AF4 = aF4;
            this.Attention = attention;
            this.Engagement = engagement;
            this.Excitement = excitement;
            this.Interest = interest;
            this.Relaxation = relaxation;
            this.Stress = stress;
            this.Battery = battery;
            this.ContactQuality = contactQuality;
            this.SlideIndex = slideIndex;
            this.SetIndex = setIndex;
            this.RowIndex = rowIndex;
        }
    }
}
