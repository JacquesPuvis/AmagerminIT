using AmagerminIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AmagerminIT.Pages
{
    public class PersonalPageModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public PersonalPageModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public User CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            if (CurrentUser == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }
    }
}