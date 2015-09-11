using System.Collections.Generic;
using FailureMap.DbModel;

namespace FailureMap.EntityFramework
{
    public class SampleInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            context.Users.Add(new User()
            {
                Username = "this is a sample",
                Documents = new List<Document>
                {
                    new Document() { IsPublic = true, Title = "Test" },
                    new Document() { IsPublic = false, Title = "Test2" },
                    new Document() { IsPublic = true, Title = "Test3" }
                }
            });

            context.SaveChanges();
        }
    }
}