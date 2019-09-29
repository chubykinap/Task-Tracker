using System.Runtime.Serialization;

namespace TaskTracker.Models
{
    [DataContract]
    public class ProjectModel
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Description { set; get; }

        [DataMember]
        public Account Account { set; get; }
    }
}