using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using AmagerminIT.Models;  // Your User class namespace

namespace AmagerminIT.Pages
{
    public class Admin : PageModel
    {
        private readonly UserManager<User> _userManager;

        public Admin(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.IsAdmin != true)
            {
                return RedirectToPage("/Home"); 
            }

            return Page();
        }
    }
}