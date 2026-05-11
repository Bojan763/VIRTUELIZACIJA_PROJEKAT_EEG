using Common;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EEGService : IEEGService
    {
        private EEGValidator validator = new EEGValidator(); //za validacije

        public AckResponse EndSession()
        {
            return new AckResponse
            {
                Success = true,
                Message = "Session ended",
                Status = "COMPLETED"

            };
        }

        public AckResponse PushSample(EegSample sample)
        {
            validator.ValidateSample(sample);

            return new AckResponse
            {
                Success = true,
                Message = "Sample received",
                Status = "IN_PROGRESS"

            };
        }

        public AckResponse StartSession(EegMeta meta)
        {
            return new AckResponse
            {
                Success = true,
                Message = "Session started",
                Status = "IN_PROGRESS"

            };
        }
    }
}
