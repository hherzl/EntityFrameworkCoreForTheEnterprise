namespace RothschildHouse.Application.Core
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> Paging<TEntity>(this IQueryable<TEntity> query, int pageSize = 0, int pageNumber = 0) where TEntity : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
}
