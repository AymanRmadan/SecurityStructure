using InfrastructureLayer.Entities;
using InfrastructureLayer.Enums.EnumSetting;
using Microsoft.EntityFrameworkCore;
using ServicesLayer.Features.Responses;

namespace ServicesLayer.Features.Mapping
{
    public static class GetAllPaginationMapping
    {
        public static async Task<List<GetAllPaginationResponse>> MapToGetAllPaginationResponse(this IQueryable<Test> query)
        {
            var result = await query.Select(test => new GetAllPaginationResponse
            {
                Id = test.Id,
                Name = test.Name,
                Phone = test.Phone,
                Address = test.Address,
                Degree = test.Degree.ToArabicValue(),
                AliasNames = test.AliasNames.Select(alias => new AliasName
                {
                    Id = alias.Id,
                    Name = alias.Name,
                }).ToList()
            }).ToListAsync();

            return result;
        }
    }
}
