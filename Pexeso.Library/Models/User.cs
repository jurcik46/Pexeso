using System.Runtime.Serialization;

namespace Pexeso.Library.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool InGame { get; set; }

        [DataMember]
        public string HostName { get; set; }


    }
}
