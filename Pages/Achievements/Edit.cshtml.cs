using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmagerminIT.Data;
using AmagerminIT.Models;

namespace AmagerminIT.Pages.Achievements
{
    public class EditModel : PageModel
    {
        private readonly AmagerminIT.Data.AmagerminITContext _context;

        public EditModel(AmagerminIT.Data.AmagerminITContext context)
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

            var achievements =  await _context.Achievements.FirstOrDefaultAsync(m => m.AchievementId == id);
            if (achievements == null)
            {
                return NotFound();
            }
            Achievements = achievements;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Achievements).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementsExists(Achievements.AchievementId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AchievementsExists(int id)
        {
            return _context.Achievements.Any(e => e.AchievementId == id);
        }
    }
}
