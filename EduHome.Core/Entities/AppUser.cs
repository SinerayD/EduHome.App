using System;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
