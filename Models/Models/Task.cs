﻿using System;
using System.Runtime.Serialization;

namespace TaskTracker.Models
{
    [DataContract]
    public class Task
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
        public virtual Account Account { set; get; }

        [DataMember]
        public virtual Project Project { set; get; }
    }

    public enum TaskStatus
    {
        Незавершён = 0, Завершён = 1
    }
}