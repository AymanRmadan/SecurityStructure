namespace ServicesLayer.Helpers
{
    public static class PaginationHelpers
    {
        public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> query, int pageNumber, int pageSize)
        {
            query = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

            return query;
        }
    }
}
