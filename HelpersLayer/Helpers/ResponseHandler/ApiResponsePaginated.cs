namespace HelpersLayer.Helpers.ResponseHandler
{
    public class ApiResponsePaginated<T> : ApiResponse<IEnumerable<T>>, IApiResponse<IEnumerable<T>>
    {
        public ApiResponsePaginated()
        {

        }
        public ApiResponsePaginated(
            IEnumerable<T> data,
            int totalCount,
            int page = 1,
            int pageSize = 10, string? message = null)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = page;
            PageSize = pageSize;
            Message = message;
        }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
