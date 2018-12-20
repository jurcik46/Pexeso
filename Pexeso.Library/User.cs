using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Library
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string IpAddress { get; set; }

        [DataMember]
        public string HostName { get; set; }
    }
}
