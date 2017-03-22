using System.Data.Entity;
using SQLite.CodeFirst;

namespace Splunk.DAL
{
    public class SplunkDbInitializer: SqliteCreateDatabaseIfNotExists<SplunkDbContext>
    {
        public SplunkDbInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public SplunkDbInitializer(DbModelBuilder modelBuilder, bool nullByteFileMeansNotExisting) : base(modelBuilder, nullByteFileMeansNotExisting)
        {
        }

        protected override void Seed(SplunkDbContext context)
        {
            base.Seed(context);
            context.Users.Add(new User
            {
                Name = "Test User"
            });
            context.SaveChanges();
        }
    }
}