using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IEEGService
    {
        [OperationContract]
        [FaultContract(typeof(DataFormatFault))]
        [FaultContract(typeof(ValidationFault))]
        AckResponse PushSample(EegSample sample);

        [OperationContract]
        AckResponse StartSession(EegMeta meta);
        
        [OperationContract]
        AckResponse EndSession();
    }
}
