using System.Runtime.Serialization;

namespace TaskTracker.Models
{
    [DataContract]
    public class Project
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public int AccountId { set; get; }

        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Description { set; get; }

        [DataMember]
        public virtual Account Account { set; get; }
    }
}