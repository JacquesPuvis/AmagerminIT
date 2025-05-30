using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmagerminIT.Data;
using AmagerminIT.Models;

namespace AmagerminIT.Pages.Achievements
{
    public class DeleteModel : PageModel
    {
        private readonly AmagerminIT.Data.AmagerminITContext _context;

        public DeleteModel(AmagerminIT.Data.AmagerminITContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Achievement Achievements { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements.FirstOrDefaultAsync(m => m.AchievementId == id);

            if (achievements == null)
            {
                return NotFound();
            }
            else
            {
                Achievements = achievements;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements.FindAsync(id);
            if (achievements != null)
            {
                Achievements = achievements;
                _context.Achievements.Remove(Achievements);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
