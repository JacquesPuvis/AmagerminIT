using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmagerminIT.Data;
using AmagerminIT.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AmagerminIT.Pages.Achievements
{
    public class AssignModel : PageModel
    {
        private readonly AmagerminITContext _context;

        public AssignModel(AmagerminITContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SelectedUserId { get; set; }

        [BindProperty]
        public int SelectedAchievementId { get; set; }

        public List<SelectListItem> UserSelectList { get; set; }

        public List<SelectListItem> AchievementSelectList { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            // Load users for dropdown
            UserSelectList = await _context.Users
                .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.DisplayName })
                .ToListAsync();

            // Load achievements for dropdown
            AchievementSelectList = await _context.Achievements
                .Select(a => new SelectListItem { Value = a.AchievementId.ToString(), Text = a.Name })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload dropdowns in case of error
            await OnGetAsync();

            if (string.IsNullOrEmpty(SelectedUserId) || SelectedAchievementId == 0)
            {
                Message = "Please select both a user and an achievement.";
                return Page();
            }

            // TODO: Add your logic to assign the achievement to the user here.
            // Assuming you have a join table UserAchievements or similar:

            var userId = SelectedUserId;
            var achievementId = SelectedAchievementId;

            // Example: check if assignment already exists
            bool alreadyAssigned = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);

            if (alreadyAssigned)
            {
                Message = "This achievement is already assigned to this user.";
                return Page();
            }

            var userAchievement = new UserAchievement
            {
                UserId = userId,
                AchievementId = achievementId,
                AssignedDate = System.DateTime.UtcNow
            };

            _context.UserAchievements.Add(userAchievement);
            await _context.SaveChangesAsync();

            Message = "Achievement assigned successfully!";
            return Page();
        }
    }
}
