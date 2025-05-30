using AmagerminIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AmagerminIT.Data;

namespace AmagerminIT.Pages
{
    public class PersonalPageModel : PageModel
    {
        private readonly AmagerminITContext _context;

        private readonly UserManager<User> _userManager;

        public PersonalPageModel(AmagerminITContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public User CurrentUser { get; set; }
        public ICollection<UserAchievement> UserAchievements { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _context.Users
                .Include(u => u.UserAchievements)              // Include the join table
                .ThenInclude(ua => ua.Achievement)         // Include the related Achievement
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (CurrentUser == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            UserAchievements = CurrentUser.UserAchievements; // assign here for convenience

            return Page();
        }

    }
}