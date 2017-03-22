using System.Data.Entity;
using SQLite.CodeFirst;

namespace Splunk.DAL
{
    public class SplunkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SplunkDbContext() : base("SqlLiteContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var model = modelBuilder.Build(Database.Connection);
            //IDatabaseCreator sqliteDatabaseCreator = new SqliteDatabaseCreator();
            //sqliteDatabaseCreator.Create(Database, model);

            var sqliteConnectionInitializer = new SplunkDbInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}