namespace Store.Core.DataLayer.Repositories
{
    public abstract class Repository
    {
        protected StoreDbContext DbContext;

        public Repository(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
