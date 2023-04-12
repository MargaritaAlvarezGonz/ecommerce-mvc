using Microsoft.AspNetCore.Identity;

namespace Blosom_API2.Models
{
    public class UserAplication : IdentityUser
    {
        public string Name { get; set; }
    }
}
