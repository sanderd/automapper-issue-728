using System.Data.Entity;
using FailureMap.DbModel;

namespace FailureMap.EntityFramework
{
    public class SampleContext : DbContext
    {
        public SampleContext() : base("FooBarConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}