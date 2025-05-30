using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AmagerminIT.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        
        public bool IsAdmin { get; set; }
    }
}