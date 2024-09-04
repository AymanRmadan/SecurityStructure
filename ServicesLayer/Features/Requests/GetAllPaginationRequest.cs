﻿namespace ServicesLayer.Features.Requests
{
    public record GetAllPaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }

}
