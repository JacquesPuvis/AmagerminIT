using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AmagerminIT.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Achievements> Achievements { get; set; } = new List<Achievements>();
        
        public bool? IsAdmin { get; set; }
    }
}