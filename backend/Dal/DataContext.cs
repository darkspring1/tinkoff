using System.Data.Entity;
using Business.Entities;

namespace Dal
{
    public class DataContext : DbContext
    {
        const string schema = "public";

        public DataContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<DataContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            //this.Database.Log = q => logger.Info(q);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Url>().ToTable("Urls", schema);
        }
        /*
        public void ChangeObjectState(object entity, EntityState entityState)
        {
            var t = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.ChangeObjectState(entity, entityState);
        }
        */
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
