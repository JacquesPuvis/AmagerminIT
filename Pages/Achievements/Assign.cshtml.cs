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

        [BindProperty]
        public int AchievementToUnassignId { get; set; }

        public List<SelectListItem> UserSelectList { get; set; }

        public List<SelectListItem> AchievementSelectList { get; set; }

        public List<UserAchievement> AssignedAchievements { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync(string selectedUserId = null)
        {
            // Load users for dropdown
            UserSelectList = await _context.Users
                .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.DisplayName })
                .ToListAsync();

            // Load achievements for dropdown
            AchievementSelectList = await _context.Achievements
                .Select(a => new SelectListItem { Value = a.AchievementId.ToString(), Text = a.Name })
                .ToListAsync();

            if (!string.IsNullOrEmpty(selectedUserId))
            {
                SelectedUserId = selectedUserId;

                // Load assigned achievements for selected user
                AssignedAchievements = await _context.UserAchievements
                    .Include(ua => ua.Achievement)
                    .Where(ua => ua.UserId == selectedUserId)
                    .ToListAsync();
            }
            else
            {
                AssignedAchievements = new List<UserAchievement>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload dropdowns and assigned achievements
            await OnGetAsync(SelectedUserId);

            if (string.IsNullOrEmpty(SelectedUserId) || SelectedAchievementId == 0)
            {
                Message = "Please select both a user and an achievement.";
                return Page();
            }

            // Check if already assigned
            bool alreadyAssigned = await _context.UserAchievements
                .AnyAsync(ua => ua.UserId == SelectedUserId && ua.AchievementId == SelectedAchievementId);

            if (alreadyAssigned)
            {
                Message = "This achievement is already assigned to this user.";
                return Page();
            }

            var userAchievement = new UserAchievement
            {
                UserId = SelectedUserId,
                AchievementId = SelectedAchievementId,
                AssignedDate = System.DateTime.UtcNow
            };

            _context.UserAchievements.Add(userAchievement);
            await _context.SaveChangesAsync();

            Message = "Achievement assigned successfully!";

            // Reload assigned achievements
            await OnGetAsync(SelectedUserId);

            return Page();
        }

        public async Task<IActionResult> OnPostUnassignAsync()
        {
            if (string.IsNullOrEmpty(SelectedUserId) || AchievementToUnassignId == 0)
            {
                Message = "Please select a user and an assigned achievement to unassign.";
                await OnGetAsync(SelectedUserId);
                return Page();
            }

            var assignment = await _context.UserAchievements
                .FirstOrDefaultAsync(ua => ua.UserId == SelectedUserId && ua.AchievementId == AchievementToUnassignId);

            if (assignment == null)
            {
                Message = "This achievement is not assigned to the selected user.";
                await OnGetAsync(SelectedUserId);
                return Page();
            }

            _context.UserAchievements.Remove(assignment);
            await _context.SaveChangesAsync();

            Message = "Achievement unassigned successfully!";

            // Reload assigned achievements
            await OnGetAsync(SelectedUserId);

            return Page();
        }
    }
}
