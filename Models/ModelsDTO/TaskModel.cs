using System;
using System.Runtime.Serialization;

namespace TaskTracker.Models
{
    [DataContract]
    public class TaskModel
    {
        [DataMember]
        public int Id { set; get; }
        
        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Description { set; get; }

        [DataMember]
        public DateTime? Shedule { set; get; }

        [DataMember]
        public TaskStatus Status { set; get; }

        [DataMember]
        public Account Account { set; get; }

        [DataMember]
        public Project Project { set; get; }
    }
}