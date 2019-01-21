using System.Data.Entity;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TrackerContext : DbContext
    {
        public virtual DbSet<Account> Accounts { set; get; }

        public virtual DbSet<Project> Projects { set; get; }

        public virtual DbSet<Task> Tasks { set; get; }
    }
}