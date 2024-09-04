using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace InfrastructureLayer.Context
{
    public class ApplicationUser : IdentityUser<int>//, IEntity<int>
    {
        // public Student? Student { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
        public string? ResetPasswordToken { get; set; }
        public string? Password { get; set; }
        public DateTime? ResetPasswordExpiry { get; set; }
    }
}
