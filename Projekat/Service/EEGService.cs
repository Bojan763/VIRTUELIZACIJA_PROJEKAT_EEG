using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EEGService : IEEGService
    {
        public AckResponse EndSession()
        {
            throw new NotImplementedException();
        }

        public AckResponse PushSample(EegMeta meta)
        {
            throw new NotImplementedException();
        }

        public AckResponse StartSession(EegMeta meta)
        {
            throw new NotImplementedException();
        }
    }
}
