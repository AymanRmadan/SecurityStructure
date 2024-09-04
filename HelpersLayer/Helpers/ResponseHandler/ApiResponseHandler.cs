namespace HelpersLayer.Helpers.ResponseHandler
{
    public static class ApiResponseHandler
    {
        public static ApiResponse<T> Deleted<T>(T entity)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        public static ApiResponse<T> Deleted<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = Message ?? "Deleted Successfully"
            };
        }
        public static ApiResponse<T> Success<T>(T data, string? Message = null, object? Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = data,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = Message ?? "Success",
                Meta = Meta
            };
        }

        public static ApiResponse<T> Success<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = Message == null ? "Successfully" : Message,

            };
        }
        public static ApiResponsePaginated<T> Success<T>(List<T> data, int totalCount, int pageNumber, int pageSize)
        {
            return new ApiResponsePaginated<T>()
            {
                Data = data,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
        public static ApiResponse<T> UnAuthorized<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Message = Message ?? "Unauthorized"
            };
        }
        public static ApiResponse<T> BadRequest<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = Message ?? "Bad Request"
            };
        }
        public static ApiResponse<T> Forbidden<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                Message = Message ?? "Forbidden"
            };
        }


        public static ApiResponse<T> UnprocessableEntity<T>(T entity)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Data = entity,
            };
        }

        public static ApiResponse<T> NotFound<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Message = Message ?? "Not Found",

            };
        }
        public static ApiResponse<T> NotFound<T>(T entity, string? Message = null, object? Meta = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = Message ?? "Not Found",
                Meta = Meta,
                Data = entity
            };
        }

        public static ApiResponse<T> Created<T>(T entity, string? Message = null, object? Meta = null)
        {
            return new ApiResponse<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = Message ?? "Created Successfully",
                Meta = Meta
            };
        }
        public static ApiResponse<T> Created<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = Message ?? "Created Successfully",
            };
        }
        public static ApiResponse<T> NoContent<T>()
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Not Content"

            };
        }
        public static ApiResponse<T> NoContent<T>(string? Message = null)
        {
            return new ApiResponse<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = Message ?? "Not Content"
            };
        }
    }


    public static class ApiPaginatedResponseHandler
    {
        public static ApiResponsePaginated<T> NotFoundPaginate<T>(string? Message = null)
        {
            return new ApiResponsePaginated<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Message = Message ?? "Not Found",

            };
        }
        public static ApiResponsePaginated<T> NotFoundPaginate<T>(List<T> entity, string? Message = null, object? Meta = null)
        {
            return new ApiResponsePaginated<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Message = Message ?? "Not Found",
                Meta = Meta,
                Data = entity
            };
        }
    }

}
