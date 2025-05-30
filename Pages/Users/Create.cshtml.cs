using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmagerminIT.Data;
using AmagerminIT.Models;

namespace AmagerminIT.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AmagerminIT.Data.AmagerminITContext _context;

        public CreateModel(AmagerminIT.Data.AmagerminITContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _context.User.Add(User);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error saving user: " + ex.Message);
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
