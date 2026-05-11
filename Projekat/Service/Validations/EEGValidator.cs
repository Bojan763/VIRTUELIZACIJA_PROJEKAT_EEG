using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;
using System.Data;

namespace Service.Validations
{
    public class EEGValidator
    {
        private int lastRowIndex = -1;

        public void ValidateSample(EegSample sample)
        {
            if(sample == null)
            {
                throw new FaultException<DataFormatFault>(
                    new DataFormatFault { 
                        Message = "Sample is null" });
            }
            ValidateTimestamp(sample);
            ValidateRowIndex(sample);
            ValidateBattery(sample);
            ValidateBattery(sample);
            ValidateContactQuality(sample);
            ValidateMetrics(sample);
        }
        private void ValidateTimestamp(EegSample sample)
        {
            if(sample.TimeStamp == default)
            {
                throw new FaultException<DataFormatFault>(
                    new DataFormatFault
                    {
                        Message = "Invalid timestamp"
                    });
            }
        }
        private void ValidateRowIndex(EegSample sample)
        {
            if (sample.RowIndex <= lastRowIndex)
            {
                throw new FaultException<ValidationFault>(
                    new ValidationFault
                    {
                        Message = "Row index must grow monotonically"
                    });
            }
            sample.RowIndex = lastRowIndex;
        }
        private void ValidateBattery(EegSample sample)
        {
            if (sample.Battery < 0 || sample.Battery > 100)
            {
                throw new FaultException<ValidationFault>(
                    new ValidationFault
                    {
                        Message = "Battery out of range"
                    });
            }
        }
        private void ValidateContactQuality(EegSample sample)
        {
            if (sample.ContactQuality < 0 || sample.ContactQuality > 100)
            {
                throw new FaultException<ValidationFault>(
                    new ValidationFault
                    {
                        Message = "Contact quality out of range"
                    });
            }
        }
        private void ValidateMetrics(EegSample sample)
        {
            ValidateRange(sample.Attention, 0, 100, "Attention");
            ValidateRange(sample.Stress, 0, 100, "Stress");
            ValidateRange(sample.Engagement, 0, 100, "Engagement");
            ValidateRange(sample.Relaxation, 0, 100, "Relaxation");
            ValidateChannel(sample.AF3, "AF3");
            ValidateChannel(sample.T7, "T7");
            ValidateChannel(sample.Pz, "Pz");
            ValidateChannel(sample.T8, "T8");
            ValidateChannel(sample.AF4, "AF4");
        }
        private void ValidateRange(double value, double min, double max, string field)
        {
            if (value < min || value > max)
            {
                throw new FaultException<ValidationFault>(
                    new ValidationFault
                    {
                        Message = $"{field} quality out of range"
                    });
            }
        }
        private void ValidateChannel(double value, string field)
        {
            if (value < -100000 || value > 100000)
            {
                throw new FaultException<ValidationFault>(
                    new ValidationFault
                    {
                        Message = $"{field} quality out of realistic range"
                    });
            }
        }
    }
}
