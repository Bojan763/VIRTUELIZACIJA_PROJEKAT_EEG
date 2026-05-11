using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class AckResponse
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember] 
        public string Message {  get; set; }
        [DataMember]
        public string Status { get; set; }
        public AckResponse(bool success, string message, string status)
        {
            this.Success = success;
            this.Message = message;
            this.Status = status;
        } 
        public AckResponse() 
        { 
        
        }
    }
}
