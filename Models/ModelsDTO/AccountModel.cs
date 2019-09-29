using System.Runtime.Serialization;

namespace TaskTracker.Models
{
    [DataContract]
    public class AccountModel
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public string Login { set; get; }

        [DataMember]
        public string Password { set; get; }
    }
}