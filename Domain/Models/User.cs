using Microsoft.AspNetCore.Identity;

namespace chat.Domain.Models
{
    public class UserAuth : IdentityUser
    {
        public string Id { get; set; }
        public int Year { get; set; }
    }
}