using InfrastructureLayer.Context;
using ServicesLayer.Features.Accounts.Responses;

namespace ServicesLayer.Features.Accounts.Mapping
{
    public static class AddRegistrationMapping
    {
        public static AddRegistrationResponse MapToAddRegistrationResponse(this ApplicationUser user)
        {
            return new AddRegistrationResponse
            {
                Id = user.Id,
                // Add other mappings as needed
            };
        }
    }
}
